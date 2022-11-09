using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ASU.Database
{
    public class ASUDatabaseTable<T> : IDatabaseTable<T> where T : class, new()
    {
        private readonly ASUContext _context;
        private readonly IClaimsService _claimsService;

        public ASUDatabaseTable(
            ASUContext context,
            IClaimsService claimsService)
        {
            _context = context;
            _claimsService = claimsService;
        }

        public IQueryable<T> Include(Expression<Func<T, object>> include)
        {
            return Queryable().Include(include);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public virtual int Count()
        {
            return _context.Set<T>().Count();
        }

        public T GetFirst(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T GetFirst(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual IQueryable<T> Queryable()
        {
            return _context.Set<T>();
        }

        public virtual void Commit()
        {
            AuditChanges();
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToArrayAsync(cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().CountAsync(cancellationToken);
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToArrayAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            return await includeProperties.Aggregate(query, (current, include) => current.Include(include))
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = new CancellationToken())
        {
            await _context.AddAsync(entity, cancellationToken);
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = new CancellationToken())
        {
            _context.Remove(entity);
            return Task.FromResult(0);
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            AuditChanges();
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CommitAsync()
        {
            AuditChanges();
            await _context.SaveChangesAsync();
        }

        private void AuditChanges()
        {
            var userId = _claimsService.UserId;
            var now = DateTime.UtcNow;

            foreach (var auditableEntity in _context.ChangeTracker.Entries<AuditableUtcEntity>())
            {
                if (auditableEntity.State == EntityState.Added ||
                    auditableEntity.State == EntityState.Modified)
                {
                    auditableEntity.Entity.UpdatedBy = userId;
                    auditableEntity.Entity.UpdatedOnUtc = now;

                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.CreatedBy = userId;
                        auditableEntity.Entity.CreatedOnUtc = now;
                    }
                }
            }
        }
    }
}

using System.Linq.Expressions;

namespace ASU.Core.Database
{
    public interface IDatabaseTable<T> where T : class, new()
    {
        IQueryable<T> Queryable();
        IQueryable<T> Include(Expression<Func<T, object>> include);
        IEnumerable<T> GetAll();
        int Count();
        T GetFirst(Expression<Func<T, bool>> predicate);
        T GetFirst(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Commit();
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> CountAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
        IEnumerable<T> BulkDelete(IEnumerable<T> entities);
        IEnumerable<T> BulkDeleteWhere(Expression<Func<T, bool>> predicate);
        IEnumerable<T> BulkAdd(IEnumerable<T> entities);
        IEnumerable<T> BulkUpdate(IEnumerable<T> entities);
    }
}

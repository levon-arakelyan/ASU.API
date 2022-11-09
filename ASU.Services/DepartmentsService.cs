using ASU.Core.Database.Entities;
using ASU.Core.Database;
using ASU.Core.DTO;
using AutoMapper;
using ASU.Core.Services;
using ASU.Core.Models;
using Microsoft.EntityFrameworkCore;
using ASU.Services.Utilities;
using ASU.Core.Enums;

namespace ASU.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDatabaseTable<Department> _departmentsTable;
        private readonly IMapper _mapper;
        private const string ErrorNoTeacherGiven = "No data given to add.";

        public DepartmentsService(IDatabaseTable<Department> departmentsTable, IMapper mapper)
        {
            _departmentsTable = departmentsTable;
            _mapper = mapper;
        }

        public async Task Add(DepartmentDTO departmentDto)
        {
            if (departmentDto == null)
            {
                throw new ArgumentNullException(ErrorNoTeacherGiven);
            }

            var department = _mapper.Map<DepartmentDTO, Department>(departmentDto);
            await _departmentsTable.AddAsync(department);
            await _departmentsTable.CommitAsync();
        }

        public async Task<PagedItemsList<DepartmentDTO>> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string filter = "")
        {
            var query = _departmentsTable
                .Queryable()
                .Include(x => x.Faculty)
                .Include(x => x.DepartmentHead)
                .ThenInclude(x => x.Head)
                .Include(x => x.Professions)
                .Include(x => x.Teachers)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var dividers = filter.Where(x => char.IsWhiteSpace(x) || char.IsPunctuation(x)).Distinct()
                    .ToArray();
                var keywords = filter.Split(dividers).Distinct()
                    .ToArray();
                query = keywords.Aggregate(query, FilterDepartments);
            }

            var totalRecords = await query.CountAsync();

            var allDepartments = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mappedDepartments = _mapper.Map<ICollection<Department>, ICollection<DepartmentDTO>>(allDepartments);

            return new PagedItemsList<DepartmentDTO>()
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                Order = new PagedListOrder() { Direction = direction, OrderBy = orderBy },
                Data = mappedDepartments,
            };
        }

        private IQueryable<Department> FilterDepartments(IQueryable<Department> query, string filter)
        {
            query = query.Where(x =>
                EF.Functions.Like(x.Name, $"%{filter}%"));
            return query;
        }

        private IQueryable<Department> OrderBatchCharges(IQueryable<Department> query, string orderBy, OrderDirection direction)
        {
            switch (orderBy.ToLower())
            {
                case "name":
                    return direction == OrderDirection.Ascending
                        ? query.OrderBy(p => p.Name)
                        : query.OrderByDescending(p => p.Name);
                case "id":
                default:
                    return direction == OrderDirection.Ascending
                        ? query.OrderBy(p => p.Id)
                        : query.OrderByDescending(p => p.Id);
            }
        }

    }
}

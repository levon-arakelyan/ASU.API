using ASU.Core.Database.Entities;
using ASU.Core.Database;
using ASU.Core.DTO;
using AutoMapper;
using ASU.Core.Services;
using ASU.Core.Models;
using Microsoft.EntityFrameworkCore;
using ASU.Core.Enums;
using ASU.Services.Utilities;

namespace ASU.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDatabaseTable<Department> _departmentsTable;
        private readonly IMapper _mapper;
        private readonly PagedItemsListUtility<Department, DepartmentDTO> _pagedItemsListUtility;

        private const string ErrorDepartmentNotFound = "Department with id={0} not found.";

        public DepartmentsService(IDatabaseTable<Department> departmentsTable, IMapper mapper)
        {
            _departmentsTable = departmentsTable;
            _mapper = mapper;
            _pagedItemsListUtility = new PagedItemsListUtility<Department, DepartmentDTO>(
               _mapper,
               GetQuery(),
               new string[] { "Name", "Faculty.Name", "DepartmentHead.Head.FullName" },
               new string[] { "Name", "Faculty.Name", "DepartmentHead.Head.FullName" }
           );
        }

        public async Task<DepartmentDTO> Get(int departmentId)
        {
            var department = await GetQuery().FirstOrDefaultAsync(x => x.Id == departmentId);
            if (department == null)
            {
                throw new Exception(string.Format(ErrorDepartmentNotFound, departmentId));
            }

            return _mapper.Map<Department, DepartmentDTO>(department);
        }
        public async Task Add(DepartmentDTO departmentDto)
        {
            if (departmentDto == null)
            {
                throw new ArgumentNullException(nameof(departmentDto));
            }

            var department = _mapper.Map<DepartmentDTO, Department>(departmentDto);
            await _departmentsTable.AddAsync(department);
            await _departmentsTable.CommitAsync();
        }

        public PagedItemsList<DepartmentDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            return _pagedItemsListUtility.GetPaged(page, pageSize, orderBy, direction, filter);
        }

        public async Task<ICollection<DepartmentDTO>> GetAll()
        {
            var departments = await GetQuery().ToListAsync();
            return _mapper.Map<ICollection<Department>, ICollection<DepartmentDTO>>(departments);
        }

        public async Task Add(NewDepartment newDepartment)
        {
            if (newDepartment == null)
            {
                throw new ArgumentNullException(nameof(newDepartment));
            }

            var department = _mapper.Map<NewDepartment, Department>(newDepartment);
            await _departmentsTable.AddAsync(department);
            await _departmentsTable.CommitAsync();
        }

        private IQueryable<Department> GetQuery()
        {
            return _departmentsTable
                .Queryable()
                .Include(x => x.Faculty)
                .ThenInclude(x => x.FacultyHead)
                .ThenInclude(x => x.Head)
                .Include(x => x.DepartmentHead)
                .ThenInclude(x => x.Head)
                .Include(x => x.Professions)
                .Include(x => x.Teachers)
                .AsNoTracking();
        }

    }
}

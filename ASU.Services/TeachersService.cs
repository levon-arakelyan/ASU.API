using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASU.Services
{
    public class TeachersService : ITeachersService
    {
        private readonly IDatabaseTable<Teacher> _teachersTable;
        private readonly IFacultyHeadsService _faultyHeadsService;
        private readonly IDepartmentHeadsService _departmentHeadsService;
        private readonly IMapper _mapper;

        private const string ErrorNoTeacherFound = "No teacher found.";
        private const string ErrorNoTeacherGiven = "No data given to add.";

        public TeachersService(
            IDatabaseTable<Teacher> teachersTable,
            IMapper mapper,
            IFacultyHeadsService faultyHeadsService,
            IDepartmentHeadsService departmentHeadsService
        )
        {
            _teachersTable = teachersTable;
            _mapper = mapper;
            _faultyHeadsService = faultyHeadsService;
            _departmentHeadsService = departmentHeadsService;
        }

        public async Task<TeacherDTO> Get(int? id = null, string? email = null, bool throwException = false, bool includePassword = false)
        {
            var teacher = await _teachersTable
                .Queryable()
                .Include(x => x.FacultyHead)
                .Include(x => x.DepartmentHead)
                .FirstOrDefaultAsync(t =>
                    (id == null || t.Id == id) &&
                    (email == null || t.Email == email));

            if (teacher == null)
            {
                if (throwException)
                    throw new Exception(ErrorNoTeacherFound);
                return null;
            }

            if (!includePassword)
            {
                teacher.Password = "";
            }

            return _mapper.Map<Teacher, TeacherDTO>(teacher);
        }

        public async Task Add(TeacherDTO teacherDto, bool isDepartmentHead = false, bool isFacultyHead = false)
        {
            if (teacherDto == null)
            {
                throw new Exception(ErrorNoTeacherGiven);
            }

            var teacher = _mapper.Map<TeacherDTO, Teacher>(teacherDto);
            await _teachersTable.AddAsync(teacher);
            await _teachersTable.CommitAsync();

            if (isFacultyHead)
            {
                await _faultyHeadsService.Add(teacherDto);
            }
            else if (isDepartmentHead)
            {
                await _departmentHeadsService.Add(teacherDto);
            }
        }

        public async Task<PagedItemsList<TeacherDTO>> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string filter = "")
        {
            var query = _teachersTable
                .Queryable()
                .Include(x => x.Department)
                .ThenInclude(x => x.Faculty)
                .Include(x => x.DepartmentHead)
                .Include(x => x.FacultyHead)
                .Include(x => x.TeacherSubjects)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var dividers = filter.Where(x => char.IsWhiteSpace(x) || char.IsPunctuation(x)).Distinct()
                    .ToArray();
                var keywords = filter.Split(dividers).Distinct()
                    .ToArray();
                query = keywords.Aggregate(query, FilterTeachers);
            }

            var totalRecords = await query.CountAsync();

            var allTeachers = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mappedTeachers = _mapper.Map<ICollection<Teacher>, ICollection<TeacherDTO>>(allTeachers);

            return new PagedItemsList<TeacherDTO>()
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                Order = new PagedListOrder() { Direction = direction, OrderBy = orderBy },
                Data = mappedTeachers,
            };
        }

        private IQueryable<Teacher> FilterTeachers(IQueryable<Teacher> query, string filter)
        {
            query = query.Where(x =>
                EF.Functions.Like(x.FirstName, $"%{filter}%") ||
                EF.Functions.Like(x.LastName, $"%{filter}%"));
            return query;
        }

        private IQueryable<Teacher> OrderBatchCharges(IQueryable<Teacher> query, string orderBy, OrderDirection direction)
        {
            switch (orderBy.ToLower())
            {
                case "firstname":
                    return direction == OrderDirection.Ascending
                        ? query.OrderBy(p => p.FirstName)
                        : query.OrderByDescending(p => p.FirstName);
                case "lastname":
                    return direction == OrderDirection.Ascending
                        ? query.OrderBy(p => p.LastName)
                        : query.OrderByDescending(p => p.LastName);
                case "id":
                default:
                    return direction == OrderDirection.Ascending
                        ? query.OrderBy(p => p.Id)
                        : query.OrderByDescending(p => p.Id);
            }
        }
    }
}

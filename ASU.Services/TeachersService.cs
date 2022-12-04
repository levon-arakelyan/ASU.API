using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using ASU.Services.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace ASU.Services
{
    public class TeachersService : ITeachersService
    {
        private readonly IDatabaseTable<Teacher> _teachersTable;

        private readonly IFacultyHeadsService _facultyHeadsService;
        private readonly IDepartmentHeadsService _departmentHeadsService;
        private readonly IDepartmentsService _departmentsService;
        private readonly ITeacherSubjectsService _teacherSubjectsService;

        private readonly IMapper _mapper;
        private readonly PagedItemsListUtility<Teacher, TeacherDTO> _pagedItemsListUtility;

        private const string ErrorNoTeacherFound = "No teacher found.";
        private const string ErrorNoTeacherGiven = "No data given to add.";
        private const string ErrorTeacherNotFound = "Teacher with id={0} not found";

        public TeachersService(
            IDatabaseTable<Teacher> teachersTable,
            IMapper mapper,
            IFacultyHeadsService facultyHeadsService,
            IDepartmentHeadsService departmentHeadsService,
            IDepartmentsService departmentsService,
            ITeacherSubjectsService teacherSubjectsService
        )
        {
            _teachersTable = teachersTable;
            _mapper = mapper;
            _facultyHeadsService = facultyHeadsService;
            _departmentHeadsService = departmentHeadsService;
            _departmentsService = departmentsService;
            _teacherSubjectsService = teacherSubjectsService;
            _pagedItemsListUtility = new PagedItemsListUtility<Teacher, TeacherDTO> (
                _mapper,
                GetQuery(),
                new string[] { "Department.Name", "FirstName", "LastName", "Email", "Rate" },
                new string[] { "Department.Name", "FirstName", "LastName", "Email", "Rate" }
            );
        }

        public async Task<TeacherDTO> Get(int? id = null, string? email = null, bool throwException = false, bool includePassword = false)
        {
            var teacher = await GetQuery()
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

        public async Task<ICollection<TeacherDTO>> GetBySubjectId(int subjectId)
        {
            var teachers = await GetQuery().Where(x => x.TeacherSubjects.Any(y => y.SubjectId == subjectId)).ToListAsync();
            return _mapper.Map<ICollection<Teacher>, ICollection<TeacherDTO>>(teachers);
        }

        public async Task Add(NewTeacher teacherDto)
        {
            if (teacherDto == null)
            {
                throw new Exception(ErrorNoTeacherGiven);
            }

            var teacher = _mapper.Map<NewTeacher, Teacher>(teacherDto);
            teacher.Password = AuthUtility.GenerateTemporaryTeacherPassword();
            await _teachersTable.AddAsync(teacher);
            await _teachersTable.CommitAsync();

            var department = await _departmentsService.Get(teacherDto.DepartmentId);
            if (department != null)
            {
                if (teacherDto.IsFacultyHead)
                {
                    await _facultyHeadsService.TryEdit(department.Faculty.Id, teacher.Id, true);
                }
                else if (teacherDto.IsDepartmentHead)
                {
                    await _departmentHeadsService.TryEdit(department.Id, teacher.Id, true);
                }
            }
        }

        public PagedItemsList<TeacherDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            return _pagedItemsListUtility.GetPaged(page, pageSize, orderBy, direction, filter);
        }

        public async Task Edit(int teacherId, EditTeacher teacherModel)
        {
            var teacher = await GetQuery().FirstOrDefaultAsync(x => x.Id == teacherId);
            if (teacher == null)
            {
                throw new Exception(string.Format(ErrorTeacherNotFound, teacherId));
            }

            if (teacherModel.TeacherShortInfoPatch != null)
            {
                var patch = _mapper.Map<JsonPatchDocument<TeacherShortInfo>, JsonPatchDocument<Teacher>>(teacherModel.TeacherShortInfoPatch);
                patch.ApplyTo(teacher);
                _teachersTable.Update(teacher);
                await _teachersTable.CommitAsync();
            }

            await _facultyHeadsService.TryEdit(teacher.Department.Faculty.Id, teacher.Id, teacherModel.IsFacultyHead);
            await _departmentHeadsService.TryEdit(teacher.Department.Id, teacher.Id, teacherModel.IsDepartmentHead);
            await _teacherSubjectsService.EditSubjects(teacherId, teacherModel.SubjectIds);
        }

        private IQueryable<Teacher> GetQuery()
        {
            return _teachersTable
                .Queryable()
                .Include(x => x.Department)
                .ThenInclude(x => x.Faculty)
                .Include(x => x.DepartmentHead)
                .Include(x => x.FacultyHead)
                .Include(x => x.TeacherSubjects)
                .ThenInclude(x => x.Subject);
        }
    }
}

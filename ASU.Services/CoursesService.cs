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
    public class CoursesService : ICoursesService
    {
        private readonly IDatabaseTable<Course> _coursesTable;
        private readonly IMapper _mapper;
        private readonly PagedItemsListUtility<Course, CourseDTO> _pagedItemsListUtility;

        private const string ErrorCourseNotFound = "{0} կուրսը չի գտնվել";

        public CoursesService(IDatabaseTable<Course> coursesTable, IMapper mapper)
        {
            _coursesTable = coursesTable;
            _mapper = mapper;
            _pagedItemsListUtility = new PagedItemsListUtility<Course, CourseDTO>(
               _mapper,
               GetQuery(),
               new string[] { "CourseName" },
               new string[] { "CourseName" }
           );
        }

        public async Task Add(NewCourse newCourse)
        {
            if (newCourse == null)
            {
                throw new ArgumentNullException(nameof(newCourse));
            }

            var course = _mapper.Map<NewCourse, Course>(newCourse);
            await _coursesTable.AddAsync(course);
            await _coursesTable.CommitAsync();
        }

        public async Task Edit(int courseId, EditCourse updatedCourse)
        {
            var course = await GetQuery().FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                throw new Exception(string.Format(ErrorCourseNotFound, courseId));
            }

            if (updatedCourse.CourseShortInfoPatch != null)
            {
                var patch = _mapper.Map<JsonPatchDocument<CourseShortInfo>, JsonPatchDocument<Course>>(updatedCourse.CourseShortInfoPatch);
                patch.ApplyTo(course);
                _coursesTable.Update(course);
                await _coursesTable.CommitAsync();
            }
        }

        public async Task<CourseDTO> Get(int courseId)
        {
            var course = await GetQuery().FirstOrDefaultAsync(x => x.Id == courseId);
            if (course != null)
            {
                return _mapper.Map<Course, CourseDTO>(course);
            }
            return null;
        }

        public async Task<ICollection<CourseDTO>> GetDepartmentRegularCourses(int departmentId)
        {
            var courses = await GetQuery()
                .Where(x =>
                    x.Profession.DepartmentId == departmentId &&
                    x.EducationType == EducationType.Regular)
                .ToListAsync();
            return _mapper.Map<ICollection<Course>, ICollection<CourseDTO>>(courses);
        }

        public PagedItemsList<CourseDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            return _pagedItemsListUtility.GetPaged(page, pageSize, orderBy, direction, filter);
        }

        private IQueryable<Course> GetQuery()
        {
            return _coursesTable
                .Queryable()
                .Include(x => x.Profession)
                .Include(x => x.Students);
        }
    }
}

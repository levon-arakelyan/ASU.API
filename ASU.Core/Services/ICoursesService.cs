using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface ICoursesService
    {
        Task Add(NewCourse facultyDto);
        Task<CourseDTO> Get(int courseId);
        Task<ICollection<CourseDTO>> GetDepartmentRegularCourses(int departmentId);
        Task Edit(int courseId, EditCourse course);
        PagedItemsList<CourseDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "");
    }
}

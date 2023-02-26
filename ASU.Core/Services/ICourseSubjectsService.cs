using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using Microsoft.AspNetCore.JsonPatch;

namespace ASU.Core.Services
{
    public interface ICourseSubjectsService
    {
        Task<ICollection<CourseSubjectDTO>> GetForCourse(int courseId);
        Task Save(List<Schedule> schedule);
        Task Save(int[] ids, JsonPatchDocument<ICollection<CourseSubjectDTO>> courseSubjectsPatch);
    }
}

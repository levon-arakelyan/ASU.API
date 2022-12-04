using ASU.Core.DTO;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface ISchedulesService
    {
        Task<ICollection<ScheduleDTO>> GenerateScheduleForCourse(int courseId, ICollection<SubjectForSchedule> subjects);
        Task<ICollection<ScheduleDTO>> GetForCourse(int courseId);
        Task AddForCourse(int courseId, ICollection<NewCourseSchedule> schedule);
    }
}

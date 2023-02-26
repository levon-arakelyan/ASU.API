using ASU.Core.DTO;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface ISchedulesService
    {
        Task<ICollection<ScheduleDTO>> GetScheduleForCourse(int courseId);
        Task<List<List<ScheduleClassGroup>>> GetRegularScheduleForCourse(int courseId);
        Task<List<List<ScheduleEditableClassGroup>>> GetEditableScheduleForCourse(int courseId);
        Task SaveScheduleForCourse(int courseId, ICollection<ICollection<ScheduleEditableClassGroup>> groups);
        Task DeleteScheduleForCourse(int courseId);
    }
}

using ASU.Core.DTO;

namespace ASU.Core.Services
{
    public interface IFacultyHeadsService
    {
        Task Add(TeacherDTO teacher);
    }
}

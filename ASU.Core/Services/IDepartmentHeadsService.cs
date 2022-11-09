using ASU.Core.DTO;

namespace ASU.Core.Services
{
    public interface IDepartmentHeadsService
    {
        Task Add(TeacherDTO teacher);
    }
}

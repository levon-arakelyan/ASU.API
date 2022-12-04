using ASU.Core.DTO;

namespace ASU.Core.Services
{
    public interface IDepartmentHeadsService
    {
        Task TryEdit(int departmentId, int teacherId, bool add);
    }
}

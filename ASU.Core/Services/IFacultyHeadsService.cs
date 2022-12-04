using ASU.Core.DTO;

namespace ASU.Core.Services
{
    public interface IFacultyHeadsService
    {
        Task TryEdit(int facultyId, int teacherId, bool add);
    }
}

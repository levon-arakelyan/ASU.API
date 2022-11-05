using ASU.Core.DTO;

namespace ASU.Core.Services
{
    public interface IFacultiesService
    {
        Task<FacultyDTO> Get(int facultyId);
    }
}

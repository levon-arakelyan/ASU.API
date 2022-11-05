using ASU.Core.DTO;
using ASU.Core.Services;

namespace ASU.Services
{
    public class FacultiesService : IFacultiesService
    {
        public FacultiesService()
        {

        }

        public async Task<FacultyDTO> Get(int facultyId)
        {
            return new FacultyDTO() { };
        }
    }
}

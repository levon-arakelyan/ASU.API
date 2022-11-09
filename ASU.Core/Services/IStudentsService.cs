using ASU.Core.DTO;

namespace ASU.Core.Services
{
    public interface IStudentsService
    {
        Task<StudentDTO> Get(int? id = null, string? email = null, bool throwException = false, bool includePassword = false);
    }
}

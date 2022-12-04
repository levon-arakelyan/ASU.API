using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface ISubjectsService
    {
        Task Add(NewSubject subject);
        PagedItemsList<SubjectDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "");
        Task<SubjectDTO> Get(int subjectId);
        Task<ICollection<SubjectDTO>> GetAll();
    }
}

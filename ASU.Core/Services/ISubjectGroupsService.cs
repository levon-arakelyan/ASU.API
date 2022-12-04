using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface ISubjectGroupsService
    {
        Task Add(NewSubjectGroup subjectGroup);
        Task<ICollection<SubjectGroupDTO>> GetAll();
        PagedItemsList<SubjectGroupDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "");
    }
}

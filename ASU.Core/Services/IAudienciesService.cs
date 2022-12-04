using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface IAudienciesService
    {
        Task Add(NewAudience audience);
        PagedItemsList<AudienceDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "");
        Task<ICollection<AudienceDTO>> GetAll();

    }
}

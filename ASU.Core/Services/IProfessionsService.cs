using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface IProfessionsService
    {
        Task Add(NewProfession profession);
        PagedItemsList<ProfessionDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "");
        Task<ICollection<ProfessionDTO>> GetAll();
    }
}

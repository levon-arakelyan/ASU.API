using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface IDepartmentsService
    {
        Task Add(DepartmentDTO facultyDto);
        Task<PagedItemsList<DepartmentDTO>> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string filter = "");
    }
}

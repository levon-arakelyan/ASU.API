using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface IDepartmentsService
    {
        Task<DepartmentDTO> Get(int departmentId);
        Task Add(NewDepartment department);
        PagedItemsList<DepartmentDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "");
        Task<ICollection<DepartmentDTO>> GetAll();
    }
}

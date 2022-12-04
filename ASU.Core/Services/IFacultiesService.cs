using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface IFacultiesService
    {
        Task Add(NewFaculty facultyDto);
        PagedItemsList<FacultyDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "");
        Task<ICollection<FacultyDTO>> GetAll();
    }
}

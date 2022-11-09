using ASU.Core.Database.Entities;

namespace ASU.Core.DTO
{
    public class FacultyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DepartmentDTO> Departments { get; set; }
        public FacultyHeadDTO FacultyHead { get; set; }
    }
}

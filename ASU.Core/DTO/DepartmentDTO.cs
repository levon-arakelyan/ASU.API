using ASU.Core.Database.Entities;

namespace ASU.Core.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FacultyDTO Faculty { get; set; }
        public ICollection<ProfessionDTO> Professions { get; set; }
        public ICollection<TeacherDTO> Teachers { get; set; }
        public DepartmentHeadDTO DepartmentHead { get; set; }
    }
}

using ASU.Core.Entities;

namespace ASU.Core.DTO
{
    public class FacultyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TeacherDTO Dean { get; set; }
        public ICollection<DepartmentDTO> Departments { get; set; }
        public FacultyHeadDTO FacultyHead { get; set; }
    }
}

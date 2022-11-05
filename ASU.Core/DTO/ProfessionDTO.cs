namespace ASU.Core.DTO
{
    public class ProfessionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DepartmentDTO Department { get; set; }
        public ICollection<CourseDTO> Courses { get; set; }
    }
}

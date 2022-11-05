namespace ASU.Core.DTO
{
    public class StudentSubjectDTO
    {
        public int Id { get; set; }
        public double? Mark { get; set; }
        public StudentDTO Student { get; set; }
        public SubjectDTO Subject { get; set; }
        public CourseDTO Course { get; set; }
    }
}

namespace ASU.Core.DTO
{
    public class CourseSubjectDTO
    {
        public int Id { get; set; }
        public int Credit { get; set; }
        public CourseDTO Course { get; set; }
        public SubjectDTO Subject { get; set; }
        public TeacherDTO Teacher { get; set; }
    }
}

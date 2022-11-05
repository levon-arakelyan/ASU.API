namespace ASU.Core.DTO
{
    public class TeacherSubjectDTO
    {
        public int Id { get; set; }
        public TeacherDTO Teacher { get; set; }
        public SubjectDTO Subject { get; set; }
    }
}

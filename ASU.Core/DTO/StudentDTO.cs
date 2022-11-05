using ASU.Core.Enums;

namespace ASU.Core.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public StudentDegree Degree { get; set; }
        public StudentGroup Group { get; set; }
        public CourseDTO Course { get; set; }
        public ICollection<StudentSubjectDTO> StudentSubjects { get; set; }
    }
}

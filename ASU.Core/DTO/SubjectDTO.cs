namespace ASU.Core.DTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseDTO Course { get; set; }
        public ICollection<CourseSubjectDTO> CourseSubjects { get; set; }
        public ICollection<StudentSubjectDTO> StudentSubjects { get; set; }
        public ICollection<TeacherSubjectDTO> SubjectTeachers { get; set; }
        public ICollection<ScheduleDTO> Schedule { get; set; }
    }
}

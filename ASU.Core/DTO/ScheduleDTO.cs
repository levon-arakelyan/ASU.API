using ASU.Core.Enums;

namespace ASU.Core.DTO
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public ClassNumber ClassNumber { get; set; }
        public StudentGroup StudentGroup { get; set; }
        public bool? IsFractionAbove { get; set; }
        public int Count { get; }
        public CourseDTO Course { get; set; }
        public SubjectDTO Subject { get; set; }
        public AudienceDTO Audience { get; set; }
        public TeacherDTO Teacher { get; set; }
    }
}

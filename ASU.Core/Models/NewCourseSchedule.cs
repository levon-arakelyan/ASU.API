using ASU.Core.Enums;

namespace ASU.Core.Models
{
    public class NewCourseSchedule
    {
        public DayOfWeek DayOfWeek { get; set; }
        public ClassNumber ClassNumber { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int AudienceId { get; set; }
        public int TeacherId { get; set; }
        public bool? IsFractionAbove { get; set; }
        public StudentGroup StudentGroup { get; set; }
    }
}

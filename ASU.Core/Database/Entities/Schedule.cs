using ASU.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Database.Entities
{
    public class Schedule : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int AudienceId { get; set; }
        public int TeacherId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public ClassNumber ClassNumber { get; set; }
        public StudentGroup StudentGroup { get; set; }
        public bool? IsFractionAbove { get; set; }
        public virtual int Count
        {
            get
            {
                var courseSubject = Course?.CourseSubjects?.FirstOrDefault(x => x.SubjectId == SubjectId);
                if (courseSubject != null)
                {
                    return courseSubject.Credit / 2;
                }
                return 0;
            }
        }
        public virtual Course Course { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Audience Audience { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class Subject : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CourseSubject> CourseSubjects { get; set; }
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}

using ASU.Core.Entities;
using ASU.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class Student : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public StudentDegree Degree { get; set; }
        public StudentGroup Group { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class StudentSubject : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int CourseId { get; set; }
        public double? Mark { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Course Course { get; set; }
    }
}

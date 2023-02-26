using ASU.Core.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Database.Entities
{
    public class CourseSubject : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int Credit { get; set; }
        public virtual Course Course { get; set; }
        public virtual Subject Subject { get; set; }
    }
}

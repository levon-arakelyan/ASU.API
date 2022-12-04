using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Database.Entities
{
    public class TeacherSubject : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Subject Subject { get; set; }
    }
}

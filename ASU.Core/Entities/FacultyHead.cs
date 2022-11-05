using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class FacultyHead : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public int TeacherId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Teacher Head { get; set; }
    }
}

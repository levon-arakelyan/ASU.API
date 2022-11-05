using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class DepartmentHead : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Teacher Head { get; set; }
    }
}

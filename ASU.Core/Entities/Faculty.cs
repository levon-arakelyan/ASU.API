using ASU.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class Faculty : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}

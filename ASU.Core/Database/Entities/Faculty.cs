using ASU.Core.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Database.Entities
{
    public class Faculty : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual FacultyHead FacultyHead { get; set; }
    }
}

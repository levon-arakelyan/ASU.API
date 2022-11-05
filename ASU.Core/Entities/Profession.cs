using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class Profession : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}

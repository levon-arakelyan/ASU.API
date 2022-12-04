using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Database.Entities
{
    public class SubjectGroup : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}

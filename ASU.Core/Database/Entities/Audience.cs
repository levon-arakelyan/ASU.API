using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Database.Entities
{
    public class Audience : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public bool HasComputers { get; set; }
        public bool HasProjector { get; set; }
        public bool HasBlackboard { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}

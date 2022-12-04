using ASU.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Database.Entities
{
    public class Audience : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public AudienceType Type { get; set; } = AudienceType.Common;
        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}

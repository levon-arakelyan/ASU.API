namespace ASU.Core.Entities
{
    public abstract class AuditableUtcEntity
    {
        public DateTime CreatedOnUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}

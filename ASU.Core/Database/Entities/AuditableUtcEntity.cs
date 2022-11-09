namespace ASU.Core.Database.Entities
{
    public abstract class AuditableUtcEntity
    {
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

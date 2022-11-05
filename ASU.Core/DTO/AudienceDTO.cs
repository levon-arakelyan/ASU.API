namespace ASU.Core.DTO
{
    public class AudienceDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool HasComputers { get; set; }
        public bool HasProjector { get; set; }
        public bool HasBlackboard { get; set; }
        public ICollection<ScheduleDTO> Schedule { get; set; }
    }
}

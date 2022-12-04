using ASU.Core.Enums;

namespace ASU.Core.DTO
{
    public class AudienceDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public AudienceType Type { get; set; }
        public ICollection<ScheduleDTO> Schedule { get; set; }
    }
}

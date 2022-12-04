using ASU.Core.Enums;

namespace ASU.Core.Models
{
    public class SubjectForSchedule
    {
        public int SubjectId { get; set; }
        public int Repeat { get; set; }
        public AudienceType AudienceType { get; set; }
    }
}

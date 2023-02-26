using ASU.Core.Enums;

namespace ASU.Core.Models
{
    public class ScheduleEditableClassGroup
    {
        public ScheduleClassType ClassType { get; set; }
        public ICollection<ScheduleEditableClass> Classes { get; set; }
    }
}

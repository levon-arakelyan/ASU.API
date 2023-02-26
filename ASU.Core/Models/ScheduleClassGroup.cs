using ASU.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASU.Core.Models
{
    public class ScheduleClassGroup
    {
        public ScheduleClassType ClassType { get; set; }
        public bool HasClasses { get; set; }
        public ICollection<ScheduleClass> Classes { get; set; }
    }
}

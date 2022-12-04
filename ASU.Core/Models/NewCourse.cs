using ASU.Core.Enums;

namespace ASU.Core.Models
{
    public class NewCourse
    {
        public int Number { get; set; }
        public int ProfessionId { get; set; }
        public int GroupsNumber { get; set; }
        public CourseDegree Degree { get; set; }
        public EducationType EducationType { get; set; }

    }
}

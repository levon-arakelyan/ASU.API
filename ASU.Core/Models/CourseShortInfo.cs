using ASU.Core.Enums;

namespace ASU.Core.Models
{
    public class CourseShortInfo
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int GroupsNumber { get; set; }
        public CourseDegree Degree { get; set; }
        public EducationType EducationType { get; set; }
    }
}

using ASU.Core.Enums;

namespace ASU.Core.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int GroupsNumber { get; set; }
        public string CourseName
        {
            get
            {
                return Profession.Name + " " + Number;
            }
        }
        public CourseDegree Degree { get; set; }
        public EducationType EducationType { get; set; }
        public ProfessionDTO Profession { get; set; }
        public ICollection<StudentDTO> Students { get; set; }
        public ICollection<CourseSubjectDTO> CourseSubjects { get; set; }
        public ICollection<ScheduleDTO> Schedule { get; set; }
        public ICollection<StudentSubjectDTO> StudentSubjects { get; set; }
    }
}

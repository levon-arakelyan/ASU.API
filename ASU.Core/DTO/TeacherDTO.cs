using ASU.Core.Enums;

namespace ASU.Core.DTO
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Email { get; set; }
        public string Password { get; set; } = "";
        public double Rate { get; set; }
        public TeacherDegree Degree { get; set; }
        public DepartmentDTO Department { get; set; }
        public ICollection<TeacherSubjectDTO> TeacherSubjects { get; set; }
        public ICollection<CourseSubjectDTO> CourseSubjects { get; set; }
        public ICollection<ScheduleDTO> Schedule { get; set; }
        public DepartmentHeadDTO DepartmentHead { get; set; }
        public FacultyHeadDTO FacultyHead { get; set; }

    }
}

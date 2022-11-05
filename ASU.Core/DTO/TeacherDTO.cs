using ASU.Core.Entities;

namespace ASU.Core.DTO
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int Salary { get; set; }
        public double Rate { get; set; }
        public DepartmentDTO Department { get; set; }
        public ICollection<TeacherSubjectDTO> TeacherSubjects { get; set; }
        public ICollection<CourseSubjectDTO> CourseSubjects { get; set; }
        public ICollection<ScheduleDTO> Schedule { get; set; }
        public FacultyDTO DeanForFaculty { get; set; }
        public DepartmentDTO HeadOfDepartment { get; set; }
        public DepartmentHeadDTO DepartmentHead { get; set; }
        public FacultyHeadDTO FacultyHead { get; set; }

    }
}

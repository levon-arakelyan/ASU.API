using ASU.Core.Enums;
namespace ASU.Core.Models
{
    public class NewTeacher
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double Rate { get; set; }
        public TeacherDegree Degree { get; set; }
        public int DepartmentId { get; set; }
        public bool IsDepartmentHead { get; set; }
        public bool IsFacultyHead { get; set; }
    }
}

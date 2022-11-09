using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Database.Entities
{
    public class Teacher : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public double Rate { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public virtual ICollection<CourseSubject> CourseSubjects { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual DepartmentHead DepartmentHead { get; set; }
        public virtual FacultyHead FacultyHead { get; set; }
    }
}

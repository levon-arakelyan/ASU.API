using ASU.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Database.Entities
{
    public class Course : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string CourseName {
            get
            {
                return Profession.Name + " " + Number;
            }
        }
        public int ProfessionId { get; set; }
        public int GroupsNumber { get; set; }
        public CourseDegree Degree { get; set; }
        public EducationType EducationType { get; set; }
        public virtual Profession Profession { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<CourseSubject> CourseSubjects { get; set; }
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}

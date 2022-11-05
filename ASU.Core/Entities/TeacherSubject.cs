using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class TeacherSubject
    {
        [Key]
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Subject Subject { get; set; }
    }
}

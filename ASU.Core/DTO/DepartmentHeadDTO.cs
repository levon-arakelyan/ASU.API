using ASU.Core.DTO;
using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class DepartmentHeadDTO
    {
        [Key]
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public virtual DepartmentDTO Department { get; set; }
        public virtual TeacherDTO Head { get; set; }
    }
}

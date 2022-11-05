using ASU.Core.DTO;
using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class FacultyHeadDTO
    {
        [Key]
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public int TeacherId { get; set; }
        public virtual FacultyDTO Faculty { get; set; }
        public virtual TeacherDTO Head { get; set; }
    }
}

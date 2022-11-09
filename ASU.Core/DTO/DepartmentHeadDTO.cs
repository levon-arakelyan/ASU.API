using System.ComponentModel.DataAnnotations;

namespace ASU.Core.DTO
{
    public class DepartmentHeadDTO
    {
        public int Id { get; set; }
        public DepartmentDTO Department { get; set; }
        public TeacherDTO Head { get; set; }
    }
}

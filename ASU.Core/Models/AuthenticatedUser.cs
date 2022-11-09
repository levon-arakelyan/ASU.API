using ASU.Core.Enums;

namespace ASU.Core.Models
{
    public class AuthenticatedUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public bool IsFacultyHead { get; set; }
        public bool IsDepartmentHead { get; set; }
    }
}

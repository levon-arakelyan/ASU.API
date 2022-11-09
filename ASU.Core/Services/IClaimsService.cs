using ASU.Core.Enums;

namespace ASU.Core.Services
{
    public interface IClaimsService
    {
        int? UserId { get; }
        string? Email { get; }
        UserRole? Role { get; }
    }
}

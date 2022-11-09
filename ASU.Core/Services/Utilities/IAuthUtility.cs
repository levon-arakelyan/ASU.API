using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Core.Services.Utilities
{
    public interface IAuthUtility
    {
        TokenModel GenerateToken(int userId, string email, UserRole role);
    }
}

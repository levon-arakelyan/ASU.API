using ASU.Core.Constants;
using ASU.Core.Enums;
using ASU.Core.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ASU.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User;

        public int? UserId
        {
            get
            {
                var userIdClaim = Principal?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                    return int.Parse(userIdClaim.Value);

                return null;
            }
        }
        public string? Email
        {
            get
            {
                var emailClaim = Principal?.FindFirst(ClaimTypes.Email);
                if (emailClaim != null)
                    return emailClaim.Value;

                return null;
            }
        }

        public UserRole? Role
        {
            get
            {
                var roleClaim = Principal?.FindFirst(ClaimTypes.Role);
                if (roleClaim != null)
                    return Enum.Parse<UserRole>(roleClaim.Value);

                return null;
            }
        }
    }
}

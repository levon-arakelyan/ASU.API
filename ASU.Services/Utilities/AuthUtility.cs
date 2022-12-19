using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services.Utilities;
using ASU.Core.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASU.Services.Utilities
{
    public class AuthUtility : IAuthUtility
    {
        private readonly AuthorizationSettings  _authorizationSettings;

        public AuthUtility(
            IOptions<AuthorizationSettings> keyOptions
        )
        {
            _authorizationSettings = keyOptions.Value;
        }

        public TokenModel GenerateToken(int userId, string email, UserRole role)
        {
            var secretKey = Encoding.UTF8.GetBytes(_authorizationSettings.SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Aud, _authorizationSettings.Audience),
                    new Claim(JwtRegisteredClaimNames.Iss, _authorizationSettings.Issuer),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public static string GenerateTemporaryTeacherPassword()
        {
            return BCrypt.Net.BCrypt.HashPassword("teacher");
        }
    }
}

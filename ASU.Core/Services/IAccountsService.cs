using ASU.Core.Enums;
using ASU.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASU.Core.Services
{
    public interface IAccountsService
    {
        Task<TokenModel> Login(LoginModel loginModel);
        Task<AuthenticatedUser> Get(int? userId, UserRole? role);
    }
}

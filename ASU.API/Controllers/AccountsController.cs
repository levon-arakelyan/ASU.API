using ASU.Core.Models;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASU.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public readonly IAccountsService _accountsService;
        public readonly IClaimsService _claimsService;

        public AccountsController(IAccountsService accountsService, IClaimsService claimsService)
        {
            _accountsService = accountsService;
            _claimsService = claimsService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _accountsService.Login(loginModel);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _accountsService.Get(_claimsService.UserId, _claimsService.Role);
            return Ok(result);
        }
    }
}

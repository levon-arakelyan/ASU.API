using ASU.Core.Enums;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASU.API.Controllers
{
    [Route("api/teachers")]
    [Authorize]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersService _teachersService;

        public TeachersController(ITeachersService teachersService)
        {
            _teachersService = teachersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            var result = await _teachersService.GetPaged(page, pageSize, orderBy, direction, filter);
            return Ok(result);
        }
    }
}

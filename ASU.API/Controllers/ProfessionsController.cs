using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASU.API.Controllers
{
    [Route("api/professions")]
    [Authorize]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly IProfessionsService _professionsService;

        public ProfessionsController(IProfessionsService professionsService)
        {
            _professionsService = professionsService;
        }

        [HttpGet("get-paged")]
        public IActionResult GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            var result = _professionsService.GetPaged(page, pageSize, orderBy, direction, filter);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _professionsService.GetAll();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] NewProfession profession)
        {
            await _professionsService.Add(profession);
            return Ok();
        }
    }
}

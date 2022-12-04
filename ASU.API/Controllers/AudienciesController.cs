using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASU.API.Controllers
{
    [Route("api/audiencies")]
    [Authorize]
    [ApiController]
    public class AudienciesController : ControllerBase
    {
        private readonly IAudienciesService _audienciesService;

        public AudienciesController(IAudienciesService audienciesService)
        {
            _audienciesService = audienciesService;
        }

        [HttpGet("get-paged")]
        public IActionResult GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            var result = _audienciesService.GetPaged(page, pageSize, orderBy, direction, filter);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _audienciesService.GetAll();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] NewAudience audience)
        {
            await _audienciesService.Add(audience);
            return Ok();
        }
    }
}

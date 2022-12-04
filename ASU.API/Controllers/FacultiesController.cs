using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASU.API.Controllers
{
    [Route("api/faculties")]
    [Authorize]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly IFacultiesService _facultiesService;

        public  FacultiesController(IFacultiesService facultiesService)
        {
            _facultiesService = facultiesService;
        }

        [HttpGet("get-paged")]
        public IActionResult GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            var result = _facultiesService.GetPaged(page, pageSize, orderBy, direction, filter);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _facultiesService.GetAll();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] NewFaculty faculty)
        {
            await _facultiesService.Add(faculty);
            return Ok();
        }
    }
}

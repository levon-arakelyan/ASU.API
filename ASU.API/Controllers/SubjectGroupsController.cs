using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASU.API.Controllers
{
    [Route("api/subject-groups")]
    [Authorize]
    [ApiController]
    public class SubjectGroupsController : ControllerBase
    {
        private readonly ISubjectGroupsService _subjectGroupsService;

        public SubjectGroupsController(ISubjectGroupsService subjectGroupsService)
        {
            _subjectGroupsService = subjectGroupsService;
        }

        [HttpGet("get-paged")]
        public IActionResult GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            var result = _subjectGroupsService.GetPaged(page, pageSize, orderBy, direction, filter);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _subjectGroupsService.GetAll();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] NewSubjectGroup subjectGroup)
        {
            await _subjectGroupsService.Add(subjectGroup);
            return Ok();
        }
    }
}

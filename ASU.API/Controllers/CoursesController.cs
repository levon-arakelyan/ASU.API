using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASU.API.Controllers
{
    [Route("api/courses")]
    [Authorize]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet("get-paged")]
        public IActionResult GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            var result = _coursesService.GetPaged(page, pageSize, orderBy, direction, filter);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] NewCourse course)
        {
            await _coursesService.Add(course);
            return Ok();
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> Get(int courseId)
        {
            var course = await _coursesService.Get(courseId);
            return Ok(course);
        }

        [HttpPatch("edit/{courseId}")]
        public async Task<IActionResult> Edit(int courseId, [FromBody] EditCourse course)
        {
            await _coursesService.Edit(courseId, course);
            return Ok();
        }
    }
}

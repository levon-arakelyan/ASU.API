using ASU.Core.DTO;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ASU.API.Controllers
{
    [Route("api/course-subjects")]
    [Authorize]
    [ApiController]
    public class CourseSubjectsController : ControllerBase
    {
        private readonly ICourseSubjectsService _courseSubjectsService;

        public CourseSubjectsController(
            ICourseSubjectsService courseSubjectsService
        )
        {
            _courseSubjectsService = courseSubjectsService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromQuery] string ids, [FromBody] JsonPatchDocument<ICollection<CourseSubjectDTO>> courseSubjects)
        {
            await _courseSubjectsService.Save(ids.Split(',').Select(x => int.Parse(x)).ToArray(), courseSubjects);
            return Ok();
        }

        [HttpGet("get-for-course/{courseId}")]
        public async Task<IActionResult> GetForCourse(int courseId)
        {
            var response = await _courseSubjectsService.GetForCourse(courseId);
            return Ok(response);
        }
    }
}

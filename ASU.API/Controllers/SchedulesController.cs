using ASU.Core.DTO;
using ASU.Core.Models;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASU.API.Controllers
{
    [Route("api/schedules")]
    [Authorize]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly ISchedulesService _schedulesService;

        public SchedulesController(ISchedulesService schedulesService)
        {
            _schedulesService = schedulesService;
        }

        [HttpPost]
        [Route("generate/{courseId}")]
        public async Task<IActionResult> GenerateScheduleForCourse(int courseId, [FromBody] ICollection<SubjectForSchedule> subjects)
        {
            var schedule = await _schedulesService.GenerateScheduleForCourse(courseId, subjects);
            return Ok(schedule);
        }

        [HttpGet]
        [Route("get-for-course/{courseId}")]
        public async Task<IActionResult> GetFourCourse(int courseId)
        {
            var schedule = await _schedulesService.GetForCourse(courseId);
            return Ok(schedule);
        }

        [HttpPost]
        [Route("add-for-course/{courseId}")]
        public async Task<IActionResult> AddFourCourse(int courseId, [FromBody] ICollection<NewCourseSchedule> subjects)
        {
            await _schedulesService.AddForCourse(courseId, subjects);
            return Ok();
        }
    }
}

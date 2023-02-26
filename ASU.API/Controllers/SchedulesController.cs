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

        [HttpGet]
        [Route("get-for-course/{courseId}")]
        public async Task<IActionResult> GetForCourse(int courseId)
        {
            var schedule = await _schedulesService.GetScheduleForCourse(courseId);
            return Ok(schedule);
        }

        [HttpGet]
        [Route("get-regular-for-course/{courseId}")]
        public async Task<IActionResult> GetRegularForCourse(int courseId)
        {
            var schedule = await _schedulesService.GetRegularScheduleForCourse(courseId);
            return Ok(schedule);
        }

        [HttpGet]
        [Route("get-editable-for-course/{courseId}")]
        public async Task<IActionResult> GetEditableForCourse(int courseId)
        {
            var schedule = await _schedulesService.GetEditableScheduleForCourse(courseId);
            return Ok(schedule);
        }

        [HttpPost]
        [Route("save-for-course/{courseId}")]
        public async Task<IActionResult> SaveForCourse(int courseId, [FromBody] ICollection<ICollection<ScheduleEditableClassGroup>> groups)
        {
            await _schedulesService.SaveScheduleForCourse(courseId, groups);
            return Ok();
        }

        [HttpDelete]
        [Route("delete-for-course/{courseId}")]
        public async Task<IActionResult> DeleteForCourse(int courseId)
        {
            await _schedulesService.DeleteScheduleForCourse(courseId);
            return Ok();
        }
    }
}

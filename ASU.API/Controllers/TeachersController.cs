using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;
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

        [HttpGet("get-paged")]
        public IActionResult GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            var result = _teachersService.GetPaged(page, pageSize, orderBy, direction, filter);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] NewTeacher teacher)
        {
            await _teachersService.Add(teacher);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var teacher = await _teachersService.Get(id, throwException: true);
            return Ok(teacher);
        }

        [HttpPatch("edit/{teacherId}")]
        public async Task<IActionResult> Edit(int teacherId, [FromBody] EditTeacher teacher)
        {
            await _teachersService.Edit(teacherId, teacher);
            return Ok();
        }

        [HttpGet]
        [Route("get-by-subject/{subjectId}")]
        public async Task<IActionResult> GetBySubject(int subjectId)
        {
            var teachers = await _teachersService.GetBySubject(subjectId);
            return Ok(teachers);
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teachersService.GetAll();
            return Ok(teachers);
        }
    }
}

using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASU.API.Controllers
{
    [Route("api/departments")]
    [Authorize]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;

        public DepartmentsController(
            IDepartmentsService departmentsService
        )
        {
            _departmentsService = departmentsService;
        }

        [HttpGet("get-paged")]
        public IActionResult GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            var result = _departmentsService.GetPaged(page, pageSize, orderBy, direction, filter);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _departmentsService.GetAll();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] NewDepartment department)
        {
            await _departmentsService.Add(department);
            return Ok();
        }
    }
}

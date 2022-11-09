using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASU.API.Controllers
{
    [Route("api/departments")]
    [Authorize]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;

        public DepartmentsController(IDepartmentsService departmentsService)
        {
            _departmentsService = departmentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            var result = await _departmentsService.GetPaged(page, pageSize, orderBy, direction, filter);
            return Ok(result);
        }
    }
}

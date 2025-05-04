using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.API.Base;
using SchoolManagmentSystem.Core.Features.Departments.Queries.Models;

namespace SchoolManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : AppControllerBase
    {

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(int id)
        {
            var dept = await Mediator.Send(new GetDepartmentByIdQuery(id));
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            return CustomResult(dept);
        }
    }
}

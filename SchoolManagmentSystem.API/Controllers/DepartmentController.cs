using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.API.Base;
using SchoolManagmentSystem.Core.Features.Departments.Queries.Models;
using SchoolManagmentSystem.Data.AppMetaData;

namespace SchoolManagmentSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : AppControllerBase
    {

        [HttpGet(RouterParams.DepartmentRouting.paginatedList)]
        public async Task<IActionResult> GetDepartmentByIdAsync([FromQuery] int id, int pageNum, int pageSize)
        {
            var dept = await Mediator.Send(new GetDepartmentByIdQuery(id, pageNum, pageSize));
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            return CustomResult(dept);
        }
    }
}

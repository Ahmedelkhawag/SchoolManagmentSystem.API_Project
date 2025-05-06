using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.API.Base;
using SchoolManagmentSystem.Core.Features.Departments.Commands.Models;
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
        [HttpGet(RouterParams.DepartmentRouting.list)]
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            var dept = await Mediator.Send(new GetAllDepartmentsWithOutIncludeQuery());
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            return CustomResult(dept);
        }
        [HttpPost(RouterParams.DepartmentRouting.Create)]
        public async Task<IActionResult> CreateDepartment([FromBody] AddDepartmentCommand departmentCommand)
        {
            var response = await Mediator.Send(departmentCommand);
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message, Errors = response.Errors, Data = departmentCommand });
            }
            else
            {
                return CustomResult(response);
            }


        }
        [HttpPut(RouterParams.DepartmentRouting.Update)]
        public async Task<IActionResult> UpdateDepartment([FromBody] EditDepartmentCommand departmentCommand)
        {
            var response = await Mediator.Send(departmentCommand);
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message, Errors = response.Errors, Data = departmentCommand });
            }
            else
            {
                return CustomResult(response);
            }
        }
    }
}

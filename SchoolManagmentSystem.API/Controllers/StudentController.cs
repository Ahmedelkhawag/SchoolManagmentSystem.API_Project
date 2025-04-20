using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.API.Base;
using SchoolManagmentSystem.Core.Features.students.Commads.Models;
using SchoolManagmentSystem.Core.Features.students.Queries.Models;
using SchoolManagmentSystem.Data.AppMetaData;

namespace SchoolManagmentSystem.API.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class StudentController : AppControllerBase
    {

        [HttpGet(RouterParams.StudentRouting.list)]
        public async Task<IActionResult> GetAllAsync()
        {
            var stds = await Mediator.Send(new GetStudentsListQuery());
            if (stds == null)
            {
                return NotFound("No students found.");
            }
            return Ok(stds);
        }
        [HttpGet(RouterParams.StudentRouting.GetById)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var std = await Mediator.Send(new GetStudentByIdQuery { Id = id });
            if (std == null)
            {
                return NotFound("Student not found.");
            }
            return CustomResult(std);
        }

        [HttpPost(RouterParams.StudentRouting.Create)]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentCommand studentCommand)
        {
            var response = await Mediator.Send(studentCommand);

            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message, Errors = response.Errors, Data = studentCommand });
            }
            else
            {
                return CustomResult(response);
            }
        }
        [HttpPut(RouterParams.StudentRouting.Update)]
        public async Task<IActionResult> UpdateStudent([FromBody] EditStudentCommand studentCommand, int id)
        {

            if (studentCommand.Id != id)
            {
                return BadRequest("Id in the route and body do not match.");
            }
            var response = await Mediator.Send(studentCommand);
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message, Errors = response.Errors, Data = studentCommand });
            }
            else
            {
                return CustomResult(response);
            }
        }
    }
}

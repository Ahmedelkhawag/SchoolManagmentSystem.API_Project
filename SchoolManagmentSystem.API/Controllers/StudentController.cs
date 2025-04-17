using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.Core.Features.students.Queries.Models;

namespace SchoolManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("students/all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var stds = await _mediator.Send(new GetStudentsListQuery());
            if (stds == null )
            {
                return NotFound("No students found.");
            }
            return Ok(stds);
        }
        [HttpGet("student/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var std = await _mediator.Send(new GetStudentByIdQuery{ Id = id });
            if (std == null)
            {
                return NotFound("Student not found.");
            }
            return Ok(std);
        }
    }
}

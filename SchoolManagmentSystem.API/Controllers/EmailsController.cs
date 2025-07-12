using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.API.Base;
using SchoolManagmentSystem.Core.Features.Emails.Commands.Models;
using SchoolManagmentSystem.Data.AppMetaData;

namespace SchoolManagmentSystem.API.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : AppControllerBase
    {
        [HttpPost(RouterParams.EmailRouting.SendEmail)]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailCommand command)
        {
            var response = await Mediator.Send(command);
            if (response.Succeeded)
            {
                return Ok(new
                {
                    statuscode = response.statusCode,
                    message = response.Message,
                    Errors = response.Errors,
                    Data = response.Data
                });
            }
            return BadRequest(new { message = response.Message, Errors = response.Errors });
        }



    }
}

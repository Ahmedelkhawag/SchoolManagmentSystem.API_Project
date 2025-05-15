using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.API.Base;
using SchoolManagmentSystem.Core.Features.User.Commands.Models;
using SchoolManagmentSystem.Data.AppMetaData;

namespace SchoolManagmentSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpPost(RouterParams.UserRouting.Create)]
        public async Task<IActionResult> RegisterUser([FromBody] AddUserCommand userCommand)
        {
            var response = await Mediator.Send(userCommand);
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message, Errors = response.Errors, Data = userCommand });
            }
            else
            {
                return CustomResult(response);
            }
        }
    }
}

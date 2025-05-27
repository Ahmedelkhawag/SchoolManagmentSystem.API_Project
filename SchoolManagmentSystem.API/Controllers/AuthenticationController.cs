using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.API.Base;
using SchoolManagmentSystem.Core.Features.Authentication.Commands.Models;
using SchoolManagmentSystem.Data.AppMetaData;

namespace SchoolManagmentSystem.API.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(RouterParams.AuthenticationRouting.Login)]
        public async Task<IActionResult> Login([FromForm] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message, Errors = response.Errors, Token = response.Data });
            }
            else
            {
                return CustomResult(response);
            }
        }
        [HttpPost(RouterParams.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {

            var response = await Mediator.Send(command);
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message, Errors = response.Errors, Token = response.Data });
            }
            else
            {
                return CustomResult(response);
            }
        }
    }
}

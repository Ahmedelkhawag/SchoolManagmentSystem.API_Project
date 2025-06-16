using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.API.Base;
using SchoolManagmentSystem.Core.Features.Authorization.Commands.Models;
using SchoolManagmentSystem.Data.AppMetaData;

namespace SchoolManagmentSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : AppControllerBase

    {
        [Authorize(Roles = "Admin")]
        [HttpPost(RouterParams.AuthorizationRouting.CreateRole)]
        public async Task<IActionResult> CreateRole([FromForm] AddRoleCommand command)
        {

            var response = await Mediator.Send(command);
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message });
            }
            return CustomResult(response);
        }
    }
}

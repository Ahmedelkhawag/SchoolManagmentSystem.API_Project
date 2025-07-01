using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.API.Base;
using SchoolManagmentSystem.Core.Features.Authorization.Commands.Models;
using SchoolManagmentSystem.Data.AppMetaData;

namespace SchoolManagmentSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase

    {

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

        [HttpPut(RouterParams.AuthorizationRouting.EditRole)]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand command)
        {

            var response = await Mediator.Send(command);
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message });
            }
            return CustomResult(response);
        }
        [HttpDelete(RouterParams.AuthorizationRouting.DeleteRole)]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {

            var response = await Mediator.Send(new DeleteRoleCommand(id));
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message });
            }
            return CustomResult(response);
        }




    }
}

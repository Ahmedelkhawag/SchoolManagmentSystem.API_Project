using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.API.Base;
using SchoolManagmentSystem.Core.Features.User.Commands.Models;
using SchoolManagmentSystem.Core.Features.User.Queries.Models;
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
        [HttpGet(RouterParams.UserRouting.paginatedList)]
        public async Task<IActionResult> GetPaginatedUsers([FromQuery] GetUsersListQuery query)
        {

            var response = await Mediator.Send(query);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet(RouterParams.UserRouting.GetById)]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await Mediator.Send(new GetUserByIdQuery(id));
            if (user is null)
            {
                return NotFound("user not found");
            }
            return CustomResult(user);
        }
        [HttpPut(RouterParams.UserRouting.Update)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand userCommand)
        {
            if (id != userCommand.Id)
            {
                return BadRequest("Id mismatch");
            }
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
        [HttpDelete(RouterParams.UserRouting.Delete)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {

            var response = await Mediator.Send(new DeleteUserCommand(id));
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message, Errors = response.Errors });
            }
            else
            {
                return CustomResult(response);
            }

        }
        [HttpPut(RouterParams.UserRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {

            var response = await Mediator.Send(command);
            if (response.Succeeded)
            {
                return Ok(new { statuscode = response.statusCode, message = response.Message, Errors = response.Errors });
            }
            else
            {
                return CustomResult(response);
            }
        }

    }
}

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

    }
}

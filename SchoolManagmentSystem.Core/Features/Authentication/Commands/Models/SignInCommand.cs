using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Data.Results;

namespace SchoolManagmentSystem.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<GeneralResponse<JWTAuthResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

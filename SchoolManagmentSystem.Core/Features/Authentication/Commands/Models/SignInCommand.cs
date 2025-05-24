using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<GeneralResponse<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.Authentication.Commands.Models
{
    public class ForgotPasswordCommand : IRequest<GeneralResponse<string>>
    {
        public string Email { get; set; }

    }
}

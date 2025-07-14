using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.Authentication.Commands.Models
{
    public class ResetPasswordCommand : IRequest<GeneralResponse<string>>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        // public string ConfirmPassword { get; set; }
    }
}

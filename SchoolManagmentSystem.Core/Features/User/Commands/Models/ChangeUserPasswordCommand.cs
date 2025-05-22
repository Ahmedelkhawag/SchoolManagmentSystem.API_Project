using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.User.Commands.Models
{
    public class ChangeUserPasswordCommand : IRequest<GeneralResponse<string>>
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public ChangeUserPasswordCommand(int id, string oldPassword, string newPassword, string confirmNewPassword)
        {
            Id = id;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            ConfirmNewPassword = confirmNewPassword;
        }
    }
}

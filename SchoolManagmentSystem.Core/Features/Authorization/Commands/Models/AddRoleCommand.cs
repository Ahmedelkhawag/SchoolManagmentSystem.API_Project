using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<GeneralResponse<string>>
    {
        public string RoleName { get; set; }
    }
}

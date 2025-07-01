using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Data.DTOs;

namespace SchoolManagmentSystem.Core.Features.Authorization.Commands.Models
{
    public class DeleteRoleCommand : DeleteRoleRequest, IRequest<GeneralResponse<string>>
    {

        public DeleteRoleCommand(int roleId)
        {
            RoleId = roleId;
        }
    }
}

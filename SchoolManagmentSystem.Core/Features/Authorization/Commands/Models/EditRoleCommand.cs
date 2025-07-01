using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Data.DTOs;

namespace SchoolManagmentSystem.Core.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : EditRoleRequest, IRequest<GeneralResponse<string>>
    {
    }


}

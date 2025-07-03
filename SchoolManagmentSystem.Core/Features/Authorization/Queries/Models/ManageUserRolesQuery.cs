using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Data.DTOs;

namespace SchoolManagmentSystem.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<GeneralResponse<ManageUserRolesResult>>
    {

        public int UserId { get; set; }
        public ManageUserRolesQuery(int id)
        {

            UserId = id;
        }
    }
}

using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Authorization.Queries.Result;

namespace SchoolManagmentSystem.Core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<GeneralResponse<GetRoleByIdResult>>
    {
        public int Id { get; set; }

        public GetRoleByIdQuery(int id)
        {
            Id = id;
        }
    }
}

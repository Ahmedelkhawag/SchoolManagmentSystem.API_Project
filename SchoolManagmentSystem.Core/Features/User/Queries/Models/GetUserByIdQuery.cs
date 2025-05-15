using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.User.Queries.Result;

namespace SchoolManagmentSystem.Core.Features.User.Queries.Models
{
    public class GetUserByIdQuery : IRequest<GeneralResponse<GetUserByIdQueryResponse>>
    {
        public int Id { get; set; }
        public GetUserByIdQuery()
        {

        }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}

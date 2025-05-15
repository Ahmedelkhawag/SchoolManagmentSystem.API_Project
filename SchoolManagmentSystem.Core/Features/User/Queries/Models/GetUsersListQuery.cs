using MediatR;
using SchoolManagmentSystem.Core.Features.User.Queries.Result;
using SchoolManagmentSystem.Core.Wrappers;

namespace SchoolManagmentSystem.Core.Features.User.Queries.Models
{
    public class GetUsersListQuery : IRequest<PaginatedResult<GetUsersListQueryResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

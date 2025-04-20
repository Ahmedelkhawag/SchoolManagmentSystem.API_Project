using MediatR;
using SchoolManagmentSystem.Core.Features.students.Queries.Results;
using SchoolManagmentSystem.Core.Wrappers;

namespace SchoolManagmentSystem.Core.Features.students.Queries.Models
{
    public class GetStudentListQueryPaginated : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string[]? orderBy { get; set; }
        public string? Search { get; set; }
    }
}

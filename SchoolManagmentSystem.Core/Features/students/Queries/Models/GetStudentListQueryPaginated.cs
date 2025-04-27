using MediatR;
using SchoolManagmentSystem.Core.Features.students.Queries.Results;
using SchoolManagmentSystem.Core.Wrappers;
using SchoolManagmentSystem.Data.Helpers;

namespace SchoolManagmentSystem.Core.Features.students.Queries.Models
{
    public class GetStudentListQueryPaginated : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}

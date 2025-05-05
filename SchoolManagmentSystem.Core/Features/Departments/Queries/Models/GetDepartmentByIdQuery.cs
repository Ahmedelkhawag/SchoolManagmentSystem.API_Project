using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Departments.Queries.Results;

namespace SchoolManagmentSystem.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentByIdQuery : IRequest<GeneralResponse<GetDepartmentByIdQueryResponse>>
    {
        public int Id { get; set; }
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }
        public GetDepartmentByIdQuery(int id, int pageNumber, int pageSize)
        {
            Id = id;
            StudentPageNumber = pageNumber;
            StudentPageSize = pageSize;
        }
    }
}

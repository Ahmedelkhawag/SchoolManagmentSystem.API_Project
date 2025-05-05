using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Departments.Queries.Results;

namespace SchoolManagmentSystem.Core.Features.Departments.Queries.Models
{
    public class GetAllDepartmentsWithOutIncludeQuery : IRequest<GeneralResponse<List<GetAllDepartmentsWithOutIncludeQueryResponse>>>
    {
    }
}

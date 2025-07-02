using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Authorization.Queries.Result;

namespace SchoolManagmentSystem.Core.Features.Authorization.Queries.Models
{
    public class GetRulesListQuery : IRequest<GeneralResponse<List<GetRulesListResult>>>
    {
    }
}

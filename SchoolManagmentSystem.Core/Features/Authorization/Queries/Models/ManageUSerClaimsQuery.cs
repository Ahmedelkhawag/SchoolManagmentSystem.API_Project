using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Data.Results;

namespace SchoolManagmentSystem.Core.Features.Authorization.Queries.Models
{
    public class ManageUSerClaimsQuery : IRequest<GeneralResponse<ManageUserClaimsResult>>
    {
        public int userId { get; set; }
        public ManageUSerClaimsQuery(int id)
        {

            userId = id;
        }
    }
}

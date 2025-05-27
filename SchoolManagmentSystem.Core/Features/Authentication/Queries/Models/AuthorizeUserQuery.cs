using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.Authentication.Queries.Models
{
    public class AuthorizeUserQuery : IRequest<GeneralResponse<string>>
    {
        public string AccessToken { get; set; }
    }
}

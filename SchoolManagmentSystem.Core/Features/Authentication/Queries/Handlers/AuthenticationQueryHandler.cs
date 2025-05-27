using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Authentication.Queries.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler, IRequestHandler<AuthorizeUserQuery, GeneralResponse<string>>
    {
        private readonly IStringLocalizer<SharedResourse> _Localizer;

        private readonly IAuthenticationService _authenticationService;

        public AuthenticationQueryHandler(IStringLocalizer<SharedResourse> localizer,
            IAuthenticationService authenticationService) : base(localizer)
        {
            _Localizer = localizer;

            _authenticationService = authenticationService;
        }

        async Task<GeneralResponse<string>> IRequestHandler<AuthorizeUserQuery, GeneralResponse<string>>.Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);

            if (string.IsNullOrEmpty(result) || !result.Contains("valid"))
            {
                return BadRequest<string>("Token is invalid");
            }


            return Success<string>("Token is valid");
        }
    }
}

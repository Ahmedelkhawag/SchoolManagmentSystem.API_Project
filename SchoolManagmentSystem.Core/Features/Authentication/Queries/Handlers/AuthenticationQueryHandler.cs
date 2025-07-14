using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Authentication.Commands.Models;
using SchoolManagmentSystem.Core.Features.Authentication.Queries.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler, IRequestHandler<AuthorizeUserQuery, GeneralResponse<string>>,
        IRequestHandler<ConfirmEmailQuery, GeneralResponse<string>>,
        IRequestHandler<ForgotPasswordCommand, GeneralResponse<string>>
    {


        #region fields
        private readonly IStringLocalizer<SharedResourse> _Localizer;

        private readonly IAuthenticationService _authenticationService;

        private readonly UserManager<ApplicationUser> _userManager;
        #endregion



        #region constructor
        public AuthenticationQueryHandler(IStringLocalizer<SharedResourse> localizer,
          IAuthenticationService authenticationService, UserManager<ApplicationUser> userManager) : base(localizer)
        {
            _Localizer = localizer;

            _authenticationService = authenticationService;
            _userManager = userManager;
        }
        #endregion



        #region handlers


        async Task<GeneralResponse<string>> IRequestHandler<AuthorizeUserQuery, GeneralResponse<string>>.Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);

            if (string.IsNullOrEmpty(result) || !result.Contains("valid"))
            {
                return BadRequest<string>("Token is invalid");
            }


            return Success<string>("Token is valid");
        }

        public async Task<GeneralResponse<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {

            var result = await _authenticationService.ConfirmEmailAsync(request.userId, request.code);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest<string>(_Localizer[SharedResourseKeys.EmailConfirmationFailed]);
            }
            return Success<string>(result);

        }

        public async Task<GeneralResponse<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ForgotPasswordAsync(request.Email);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest<string>(_Localizer[SharedResourseKeys.ForgotPasswordFailed]);
            }
            return Success<string>(result);
        }
        #endregion

    }
}

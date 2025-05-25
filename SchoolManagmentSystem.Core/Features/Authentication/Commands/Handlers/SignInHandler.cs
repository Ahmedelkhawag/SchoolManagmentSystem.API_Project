using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Authentication.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Data.Helpers;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Authentication.Commands.Handlers
{
    public class SignInHandler : ResponseHandler, IRequestHandler<SignInCommand, GeneralResponse<JWTAuthResponse>>
    {
        #region Feilds

        private readonly IStringLocalizer<SharedResourse> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Ctor
        public SignInHandler(IStringLocalizer<SharedResourse> localizer,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IAuthenticationService authenticationService) : base(localizer)
        {
            _localizer = localizer;
            _signInManager = signInManager;
            _userManager = userManager;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Handlers
        public async Task<GeneralResponse<JWTAuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                return NotFound<JWTAuthResponse>(_localizer[SharedResourseKeys.UserNameIsNotExist]);
            }
            //try to sign in
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            // if faild ? return password or userName is wrong try again
            if (!result.Succeeded)
            {
                return UnprocessableEntity<JWTAuthResponse>(_localizer[SharedResourseKeys.PasswordOrUserNameIsWrong]);
            }
            // if success ? generate token

            var token = await _authenticationService.GenerateJWTToken(user);
            // return token
            return Success(token, null, _localizer[SharedResourseKeys.Succeeded]);




        }
        #endregion
    }
}

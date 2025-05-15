using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.User.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Core.Features.User.Commands.Handler
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, GeneralResponse<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _localizer;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Ctor
        public UserCommandHandler(IStringLocalizer<SharedResourse> localizer,
            IMapper mapper,
            UserManager<ApplicationUser> userManager) : base(localizer)
        {
            _localizer = localizer;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Handlers
        public async Task<GeneralResponse<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //IS Email Exist
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                return BadRequest<string>(_localizer[SharedResourseKeys.AlreadyExists]);
            }
            //IS UserName Exist
            user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return BadRequest<string>(_localizer[SharedResourseKeys.AlreadyExists]);
            }
            //Map User
            var newUser = _mapper.Map<ApplicationUser>(request);

            //Create User
            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (result.Succeeded)
            {
                return Success<string>("User Added Successfully");
            }
            else
            {
                return UnprocessableEntity<string>(string.Join(",", result.Errors.Select(x => x.Description)));
            }


        }
        #endregion
    }

}

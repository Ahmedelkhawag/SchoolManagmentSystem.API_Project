using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.User.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Core.Features.User.Commands.Handler
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, GeneralResponse<string>>,
        IRequestHandler<UpdateUserCommand, GeneralResponse<string>>
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

        public async Task<GeneralResponse<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user != null)
            {
                //Map User
                var updatedUser = _mapper.Map(request, user);
                //Update User
                var result = await _userManager.UpdateAsync(updatedUser);
                //Check if the update was successful
                if (result.Succeeded)
                {
                    return Success<string>("User Updated Successfully");
                }
                else
                {
                    return UnprocessableEntity<string>(string.Join(",", result.Errors.Select(x => x.Description)));
                }

            }
            //If user not found
            else
            {
                return NotFound<string>(_localizer[SharedResourseKeys.NotFound]);
            }
        }
        #endregion
    }

}

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.User.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.User.Commands.Handler
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, GeneralResponse<string>>,
        IRequestHandler<UpdateUserCommand, GeneralResponse<string>>,
        IRequestHandler<DeleteUserCommand, GeneralResponse<string>>,
        IRequestHandler<ChangeUserPasswordCommand, GeneralResponse<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _localizer;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        #endregion

        #region Ctor
        public UserCommandHandler(IStringLocalizer<SharedResourse> localizer,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IUserService userService) : base(localizer)
        {
            _localizer = localizer;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _userService = userService;
        }
        #endregion

        #region Handlers
        public async Task<GeneralResponse<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            #region code before refactoring
            ////IS Email Exist
            //var user = await _userManager.FindByEmailAsync(request.Email);
            //if (user != null)
            //{
            //    return BadRequest<string>(_localizer[SharedResourseKeys.AlreadyExists]);
            //}
            ////IS UserName Exist
            //user = await _userManager.FindByNameAsync(request.UserName);
            //if (user != null)
            //{
            //    return BadRequest<string>(_localizer[SharedResourseKeys.AlreadyExists]);
            //}
            //Map User
            //Assign Role to User
            //await _userManager.AddToRoleAsync(newUser, "user");
            //// send confirmation email
            //var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            //var confirmationLink = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/v1/Authentication/ConfirmEmail?userId={newUser.Id}&code={confirmationCode}";
            ////message body
            //var messageBody = $"Please confirm your email by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>";
            //// Send Email
            //var sendEmailResult = await _emailService.SendEmailAsync(newUser.Email, "Elkhawaga", messageBody, "Confirm Email");
            #endregion
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userByEmail != null)
            {
                // بنرجع رد واضح إن الإيميل موجود
                return BadRequest<string>(_localizer[SharedResourseKeys.EmailIsAlreadyExists]);
            }

            var userByName = await _userManager.FindByNameAsync(request.UserName);
            if (userByName != null)
            {
                // بنرجع رد واضح إن اسم المستخدم موجود
                return BadRequest<string>(_localizer[SharedResourseKeys.UserNameIsAlreadyExists]);
            }
            try
            {
                var newUser = _mapper.Map<ApplicationUser>(request);

                //Create User
                var result = await _userService.RegisterUserAync(newUser, request.Password);
                return Success<string>(result);
            }
            catch (Exception ex)
            {

                return UnprocessableEntity<string>(ex.Message);
            }


        }

        public async Task<GeneralResponse<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var Existinguser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (Existinguser != null)
            {
                //Map User
                var updatedUser = _mapper.Map(request, Existinguser);
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

        public async Task<GeneralResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
            //If user found
            if (user != null)
            {
                //Delete User
                var result = await _userManager.DeleteAsync(user);
                //Check if the delete was successful
                if (result.Succeeded)
                {
                    return Deleted<string>();
                }
                //If delete failed
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

        public async Task<GeneralResponse<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user != null)
            {
                //Check if the old password is correct
                var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                //Check if the update was successful
                if (result.Succeeded)
                {
                    return Success<string>("User Password Updated Successfully");
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

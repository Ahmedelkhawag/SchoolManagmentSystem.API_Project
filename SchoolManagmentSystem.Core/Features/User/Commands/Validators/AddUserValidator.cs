using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.User.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;

namespace SchoolManagmentSystem.Core.Features.User.Commands.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Feilds

        private readonly IStringLocalizer<SharedResourse> _Localizer;
        #endregion


        #region Ctor
        public AddUserValidator(IStringLocalizer<SharedResourse> localizer)
        {
            _Localizer = localizer;
            ApplyValidatationsRules();
            ApplyCustomValidationRules();
        }
        #endregion


        #region Funcs
        public void ApplyValidatationsRules()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(_Localizer[SharedResourseKeys.NotEmpty])
                .MinimumLength(3)
                .WithMessage(_Localizer[SharedResourseKeys.MinimumLength]);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(_Localizer[SharedResourseKeys.NotEmpty])
                .MinimumLength(3)
                .WithMessage(_Localizer[SharedResourseKeys.MinimumLength]);
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(_Localizer[SharedResourseKeys.NotEmpty])
                .MinimumLength(3)
                .WithMessage(_Localizer[SharedResourseKeys.MinimumLength]);
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(_Localizer[SharedResourseKeys.Required]);
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage(_Localizer[SharedResourseKeys.Required])
                .Equal(x => x.Password)
                .WithMessage(_Localizer[SharedResourseKeys.PasswordDoesNotMatch]);
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(_Localizer[SharedResourseKeys.Required])
                .EmailAddress()
                .WithMessage(_Localizer[SharedResourseKeys.EmailIsNotValid]);
        }


        public void ApplyCustomValidationRules()
        {

        }
        #endregion
    }
}

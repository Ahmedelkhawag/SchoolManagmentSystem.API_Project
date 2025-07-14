using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.Authentication.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;

namespace SchoolManagmentSystem.Core.Features.Authentication.Commands.Validators
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        #endregion

        #region constructor

        public ResetPasswordCommandValidator(IStringLocalizer<SharedResourse> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }

        #endregion

        #region valdators
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.NotEmpty])
                .EmailAddress().WithMessage(_stringLocalizer[SharedResourseKeys.EmailIsNotValid]);

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.NotEmpty]);

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.NotEmpty])
                .MinimumLength(8).WithMessage(_stringLocalizer[SharedResourseKeys.PasswordLength]);
            //RuleFor(x=>x.ConfirmPassword)
            //    .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.NotEmpty])
            //    .Equal(x => x.NewPassword).WithMessage(_stringLocalizer[SharedResourseKeys.PasswordDoesNotMatch]);

        }

        #endregion
    }
}

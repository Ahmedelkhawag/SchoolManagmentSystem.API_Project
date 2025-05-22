using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.User.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;

namespace SchoolManagmentSystem.Core.Features.User.Commands.Validators
{
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResourse> _localizer;
        public ChangeUserPasswordCommandValidator(IStringLocalizer<SharedResourse> localizer)
        {

            _localizer = localizer;
            ApplyValidationrules();
        }

        public void ApplyValidationrules()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage(_localizer[SharedResourseKeys.Required])
               .GreaterThan(0).WithMessage(_localizer[SharedResourseKeys.Greaterthan]);
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourseKeys.Required])
                .MinimumLength(6).WithMessage("Old password must be at least 6 characters long");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourseKeys.Required])
                .MinimumLength(6).WithMessage("New password must be at least 6 characters long");
            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourseKeys.Required])
                .Equal(x => x.NewPassword).WithMessage(_localizer[SharedResourseKeys.PasswordDoesNotMatch]);
        }
    }
}

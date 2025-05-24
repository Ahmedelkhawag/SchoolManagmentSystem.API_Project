using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.Authentication.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;

namespace SchoolManagmentSystem.Core.Features.Authentication.Commands.Validators
{
    public class SignInValidators : AbstractValidator<SignInCommand>
    {
        private readonly IStringLocalizer<SharedResourse> _localizer;
        public SignInValidators(IStringLocalizer<SharedResourse> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {


            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_localizer[SharedResourseKeys.Required])
                .NotNull().WithMessage(_localizer[SharedResourseKeys.NotNull])
                .MinimumLength(3).WithMessage(_localizer[SharedResourseKeys.MinimumLength])
                .MaximumLength(50).WithMessage(_localizer[SharedResourseKeys.MaximumLength]);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_localizer[SharedResourseKeys.Required])
                .NotNull().WithMessage(_localizer[SharedResourseKeys.NotNull])
                .MinimumLength(6).WithMessage(_localizer[SharedResourseKeys.MinimumLength])
                .MaximumLength(100).WithMessage(_localizer[SharedResourseKeys.MaximumLength]);

        }
    }
}

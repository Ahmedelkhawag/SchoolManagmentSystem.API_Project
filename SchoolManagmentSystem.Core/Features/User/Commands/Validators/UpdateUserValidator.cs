using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.User.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;

namespace SchoolManagmentSystem.Core.Features.User.Commands.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        #region Feilds
        private readonly IStringLocalizer<SharedResourse> _Localizer;
        #endregion
        public UpdateUserValidator(IStringLocalizer<SharedResourse> Localizer)
        {
            _Localizer = Localizer;
            ApplyValidatationsRules();
            ApplyCustomValidationRules();
        }

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
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(_Localizer[SharedResourseKeys.Required])
                .EmailAddress()
                .WithMessage(_Localizer[SharedResourseKeys.EmailIsNotValid]);
        }


        public void ApplyCustomValidationRules()
        {

        }


    }
}

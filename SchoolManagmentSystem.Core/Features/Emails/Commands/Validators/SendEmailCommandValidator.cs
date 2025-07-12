using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.Emails.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;

namespace SchoolManagmentSystem.Core.Features.Emails.Commands.Validators
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        #endregion


        #region ctor

        public SendEmailCommandValidator(IStringLocalizer<SharedResourse> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion


        #region functions
        public void ApplyValidationRules()
        {

            RuleFor(x => x.ToEmail)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.Required])
                .EmailAddress().WithMessage(_stringLocalizer[SharedResourseKeys.EmailIsNotValid]);
            RuleFor(x => x.ToName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.Required]);
            RuleFor(x => x.HTMLMessage)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.Required]);
            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.Required]);
        }


        public void ApplyCustomValidationRules()
        {

        }
        #endregion
    }
}

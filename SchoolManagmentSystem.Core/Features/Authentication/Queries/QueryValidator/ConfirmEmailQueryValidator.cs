using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.Authentication.Queries.Models;
using SchoolManagmentSystem.Core.SharedResourses;

namespace SchoolManagmentSystem.Core.Features.Authentication.Queries.QueryValidator
{
    public class ConfirmEmailQueryValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;

        #endregion



        #region  constructor
        public ConfirmEmailQueryValidator(IStringLocalizer<SharedResourse> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }

        #endregion


        #region Validation Rules
        public void ApplyValidationRules()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.NotEmpty])
                .GreaterThan(0).WithMessage(_stringLocalizer["User ID must be greater than zero."]);
            RuleFor(x => x.code)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.NotEmpty]);

        }

        #endregion



    }
}

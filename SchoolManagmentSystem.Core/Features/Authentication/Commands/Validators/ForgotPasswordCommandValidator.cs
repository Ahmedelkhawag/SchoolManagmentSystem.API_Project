using FluentValidation;
using SchoolManagmentSystem.Core.Features.Authentication.Commands.Models;

namespace SchoolManagmentSystem.Core.Features.Authentication.Commands.Validators
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        #region Fields

        #endregion

        #region constructor

        #endregion

        #region valdators
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }

        #endregion
    }
}

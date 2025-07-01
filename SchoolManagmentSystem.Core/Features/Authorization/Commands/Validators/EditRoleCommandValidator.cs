using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.Authorization.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;

namespace SchoolManagmentSystem.Core.Features.Authorization.Commands.Validators
{
    public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
    {

        private readonly IStringLocalizer<SharedResourse> _localizer;

        public EditRoleCommandValidator(IStringLocalizer<SharedResourse> localizer)
        {

            _localizer = localizer;
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {

            RuleFor(command => command.Id)
                .NotEmpty().WithMessage(_localizer["Role Id is required."]);
            RuleFor(command => command.RoleName)
                .NotEmpty().WithMessage(_localizer["Role Name is required."])
                .MinimumLength(3).WithMessage("Role Name must be at least 3 characters long.")
                .MaximumLength(50).WithMessage(_localizer["Role Name must be no more than 50 characters long."]);
        }
    }
}

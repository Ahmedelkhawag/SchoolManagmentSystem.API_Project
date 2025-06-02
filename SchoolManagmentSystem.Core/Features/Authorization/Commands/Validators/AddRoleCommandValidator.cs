using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.Authorization.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Authorization.Commands.Validators
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        #region Feilds
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IAutorizationService _authorizationService;
        #endregion

        #region Ctor
        public AddRoleCommandValidator(IStringLocalizer<SharedResourse> stringLocalizer,
            IAutorizationService autorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = autorizationService;
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }
        #endregion

        #region Functions
        public void ApplyValidationRoles()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKeys.NotEmpty])
                .MinimumLength(3).WithMessage(_stringLocalizer["RoleNameMinimumLength"])
                .MaximumLength(50).WithMessage(_stringLocalizer["RoleNameMaximumLength"]);
        }

        public void ApplyCustomValidationRoles()
        {
            RuleFor(x => x.RoleName)
                .MustAsync(async (roleName, cancellation) => !await _authorizationService.IsRoleExist(roleName))
                .WithMessage(_stringLocalizer[SharedResourseKeys.AlreadyExists]);

        }
        #endregion
    }
}

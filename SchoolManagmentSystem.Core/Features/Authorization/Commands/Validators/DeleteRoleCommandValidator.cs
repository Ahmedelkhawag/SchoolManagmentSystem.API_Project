using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.Authorization.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Authorization.Commands.Validators
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields  
        private readonly IStringLocalizer<SharedResourse> _localizer;
        private readonly IAuthorizationServices _authorizationService;

        #endregion

        #region CTOR
        public DeleteRoleCommandValidator(IStringLocalizer<SharedResourse> localizer, IAuthorizationServices authorizationService)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
            ApplyValidationrules();
            ApplyCustomValidationRules();
        }

        #endregion

        #region Functions
        public void ApplyValidationrules()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage(_localizer[SharedResourseKeys.Required])
                .GreaterThan(0).WithMessage(_localizer["Role ID must be greater than zero."]);
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.RoleId)
                .MustAsync(async (roleId, cancellation) => await _authorizationService.RoleCanBeDeleted(roleId))
                .WithMessage(_localizer["You cannot delete this role."]);
        }

        #endregion

    }
}

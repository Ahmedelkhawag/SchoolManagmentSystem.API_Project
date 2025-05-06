using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.Departments.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Departments.Commands.Validators
{
    public class EditDepartmentValidator : AbstractValidator<EditDepartmentCommand>
    {
        private readonly IStringLocalizer<SharedResourse> _localizer;
        private readonly IDepartmentService _departmentService;

        public EditDepartmentValidator(IStringLocalizer<SharedResourse> localizer, IDepartmentService departmentService)
        {
            _localizer = localizer;
            _departmentService = departmentService;
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(s => s.NameAr).NotEmpty().WithMessage(_localizer[SharedResourseKeys.NotEmpty])
                  .NotNull().WithMessage(_localizer[SharedResourseKeys.NotNull])
                  .MaximumLength(50).WithMessage(_localizer[SharedResourseKeys.MaximumLength])
                  .MinimumLength(2).WithMessage(_localizer[SharedResourseKeys.MinimumLength]);
            RuleFor(s => s.NameEn).NotEmpty().WithMessage(_localizer[SharedResourseKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourseKeys.NotNull])
                .MaximumLength(50).WithMessage(_localizer[SharedResourseKeys.MaximumLength])
                .MinimumLength(2).WithMessage(_localizer[SharedResourseKeys.MinimumLength]);

            RuleFor(s => s.ManagerId).NotEmpty().WithMessage(_localizer[SharedResourseKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourseKeys.NotNull])
                .MustAsync(async (model, key, CancellationToken) => await _departmentService.IsManagerExist(model.ManagerId))
                .WithMessage(_localizer[SharedResourseKeys.NotFound]);

        }
    }
}

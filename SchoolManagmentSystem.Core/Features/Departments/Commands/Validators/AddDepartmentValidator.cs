using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.Departments.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Departments.Commands.Validators
{
    public class AddDepartmentValidator : AbstractValidator<AddDepartmentCommand>
    {

        #region FEILDS
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResourse> _localizer;
        #endregion

        #region Ctors
        public AddDepartmentValidator(IDepartmentService departmentService, IStringLocalizer<SharedResourse> localizer)
        {
            _departmentService = departmentService;
            _localizer = localizer;
            ApplyValidationRules();
        }
        #endregion

        #region fUNCTIONS   
        public void ApplyValidationRules()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(_localizer[SharedResourseKeys.NotEmpty])
                .Length(2, 150).WithMessage(_localizer[SharedResourseKeys.MaximumLength]);
            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(_localizer[SharedResourseKeys.NotEmpty])
                .Length(2, 150).WithMessage(_localizer[SharedResourseKeys.MaximumLength]);
            RuleFor(x => x.ManagerId)
                .NotNull().WithMessage(_localizer[SharedResourseKeys.NotNull]);
        }
        #endregion





    }
}

using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.students.Commads.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.students.Commads.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResourse> _localizer;

        #endregion

        #region Ctors
        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedResourse> localizer)
        {
            _studentService = studentService;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(s => s.NameAr).NotEmpty().WithMessage(_localizer[SharedResourseKeys.NotEmpty])
                  .NotNull().WithMessage(_localizer[SharedResourseKeys.NotNull])
                  .MaximumLength(50).WithMessage(_localizer[SharedResourseKeys.MaximumLength])
                  .MinimumLength(2).WithMessage(_localizer[SharedResourseKeys.MinimumLength]);

            RuleFor(s => s.Address).NotEmpty().WithMessage(_localizer[SharedResourseKeys.NotEmpty])
             .NotNull().WithMessage(_localizer[SharedResourseKeys.NotNull])
             .MaximumLength(250).WithMessage(_localizer[SharedResourseKeys.MaximumLength])
             .MinimumLength(3).WithMessage(_localizer[SharedResourseKeys.MinimumLength]);
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(s => s.NameAr)
                .MustAsync(async (key, CancellationToken) => !await _studentService.IsNameExist(key))
                .WithMessage(_localizer[SharedResourseKeys.AlreadyExists]);


        }
        #endregion

    }
}


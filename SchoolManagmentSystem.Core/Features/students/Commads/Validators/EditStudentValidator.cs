using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Features.students.Commads.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.students.Commads.Validators
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResourse> _localizer;

        #endregion

        #region Ctors
        public EditStudentValidator(IStudentService studentService, IStringLocalizer<SharedResourse> localizer)
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
            RuleFor(s => s.NameAr).NotEmpty().WithMessage("Name is Required..")
                  .NotNull().WithMessage("Name is Required And can't be null ..")
                  .MaximumLength(50).WithMessage("Name must be less than 50 characters")
                  .MinimumLength(2).WithMessage("Name must be more than 2 characters");

            RuleFor(s => s.Address).NotEmpty().WithMessage("Address is Required..")
             .NotNull().WithMessage("Address is Required..")
             .MaximumLength(250).WithMessage("Address must be less than 250 characters")
             .MinimumLength(3).WithMessage("Address must be more than 2 characters");
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(s => s.NameAr)
                .MustAsync(async (model, key, CancellationToken) => !await _studentService.IsNameExistWithDidderentId(key, model.Id))
                .WithMessage("Student with this name already exists");

            RuleFor(s => s.NameEn)
              .MustAsync(async (model, key, CancellationToken) => !await _studentService.IsNameExistWithDidderentId(key, model.Id))
              .WithMessage(_localizer[SharedResourseKeys.AlreadyExists]);

        }
        #endregion
    }
}

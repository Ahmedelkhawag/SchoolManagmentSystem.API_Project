using FluentValidation;
using SchoolManagmentSystem.Core.Features.students.Commads.Models;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.students.Commads.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;

        #endregion

        #region Ctors
        public AddStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("Name is Required..")
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
            RuleFor(s => s.Name)
                .MustAsync(async (key, CancellationToken) => !await _studentService.IsNameExist(key))
                .WithMessage("Student with this name already exists");


        }
        #endregion

    }
}


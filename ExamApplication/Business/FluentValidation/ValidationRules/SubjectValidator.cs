using ExamApplication.Entities.Concrete;
using FluentValidation;

namespace ExamApplication.Business.FluentValidation.ValidationRules
{
    public class SubjectValidator:AbstractValidator<Subject>
    {
        public SubjectValidator() 
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .Length(3)
                .WithMessage("Subject code must be exactly 3 characters.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Subject name cannot exceed 30 characters.");

            RuleFor(x => x.Class)
                .InclusiveBetween(1, 99)
                .WithMessage("Class must be between 1 and 99.");

            RuleFor(x => x.TeacherFirstName)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Teacher first name cannot exceed 20 characters.");

            RuleFor(x => x.TeacherLastName)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Teacher last name cannot exceed 20 characters.");

        }
    }
}

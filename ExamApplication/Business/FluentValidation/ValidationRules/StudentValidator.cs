using ExamApplication.Entities.Concrete;
using FluentValidation;

namespace ExamApplication.Business.FluentValidation.ValidationRules
{
    public class StudentValidator:AbstractValidator<Student>
    {
        public StudentValidator() 
        {
            RuleFor(x => x.Number)
                .GreaterThan(0)
                .WithMessage("Student number must be greater than 0.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("First name cannot exceed 30 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Last name cannot exceed 30 characters.");

            RuleFor(x => x.Class)
                .InclusiveBetween(1, 11)
                .WithMessage("Class must be between 1 and 11.");
        }

    }
}

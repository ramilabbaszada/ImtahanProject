using ExamApplication.Entities.Concrete;
using FluentValidation;

namespace ExamApplication.Business.FluentValidation.ValidationRules
{
    public class ExamValidator:AbstractValidator<Exam>
    {
        public ExamValidator() 
        {
            RuleFor(x => x.SubjectCode)
                .NotEmpty()
                .Length(3)
                .WithMessage("Subject code must be exactly 3 characters.");

            RuleFor(x => x.StudentNumber)
                .GreaterThan(0)
                .WithMessage("Student number must begreater than 0.");

            RuleFor(x => x.ExamDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Exam date cannot be in the future.");

            RuleFor(x => x.Grade)
                .InclusiveBetween(0, 11)
                .WithMessage("Grade must be between 0 and 11.");
        }
    }
}

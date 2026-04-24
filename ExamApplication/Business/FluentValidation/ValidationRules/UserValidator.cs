using ExamApplication.Business.Dtos;
using ExamApplication.Entities.Concrete;
using FluentValidation;

namespace ExamApplication.Business.FluentValidation.ValidationRules
{
    public class UserValidator:AbstractValidator<UserDto>
    {
        public UserValidator() 
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("Username must be between 3 and 30 characters.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(50)
                .WithMessage("Password must be between 6 and 50 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.");
        }
    }
}

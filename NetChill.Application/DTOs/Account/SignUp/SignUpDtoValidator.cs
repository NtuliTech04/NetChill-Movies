using FluentValidation;
using NetChill.Domain.Constants;

namespace NetChill.Application.DTOs.Account.SignUp
{
    public class SignUpDtoValidator : AbstractValidator<SignUpDto>
    {
        public SignUpDtoValidator()
        {
            RuleFor(r => r.FirstName)
               .NotEmpty().WithMessage("First Name field is required.")
               .MinimumLength(3).WithMessage("First Name cannot be less than 3 characters.")
               .MaximumLength(30).WithMessage("First Name cannot be more than 30 characters.")
               .Matches(RegexExpressions.ProperCase).WithMessage("First Name can only be proper case.");

            RuleFor(r => r.LastName)
               .NotEmpty().WithMessage("Last Name field is required.")
               .MinimumLength(3).WithMessage("Last Name cannot be less than 3 characters.")
               .MaximumLength(30).WithMessage("Last Name cannot be more than 30 characters.")
               .Matches(RegexExpressions.ProperCase).WithMessage("Last Name can only be proper case.");

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email field is required.")
                .MinimumLength(5).WithMessage("Your email address cannot be less than 5 characters long.")
                .EmailAddress().Matches(RegexExpressions.EmailRegex).WithMessage("Invalid email format.");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password field is required.")
                .Matches(RegexExpressions.PasswordRegex)
                .WithMessage("Password must be between 6 and 20 characters long with at least one uppercase, one lowercase, one digit, and one special character.");

            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().Equal(r => r.Password)
                .WithMessage("The password and confirmation password do not match.");
        }
    }
}


using FluentValidation;

namespace NetChill.Application.DTOs.Account.Login
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email field is required.");

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage("Password field is required.");
        }
    }
}

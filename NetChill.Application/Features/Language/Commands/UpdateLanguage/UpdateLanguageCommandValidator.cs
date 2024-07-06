using FluentValidation;
using NetChill.Domain.Constants;

namespace NetChill.Application.Features.Language.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageCommandValidator()
        {
            RuleFor(r => r.SpokenLanguage)
                  .NotEmpty().WithMessage("Spoken Language field is required.")
                  .Matches(RegexExpressions.ProperCase).WithMessage("Language can only be 'Proper Case'.");

            RuleFor(r => r.LanguageNotes)
                .NotEmpty().WithMessage("Language Notes field is required.")
                .MaximumLength(225).WithMessage("Language Notes must not exceed 255 characters.")
                .Matches(RegexExpressions.SentenceCase).WithMessage("Language Notes can only be 'Sentence case'.");
        }
    }
}

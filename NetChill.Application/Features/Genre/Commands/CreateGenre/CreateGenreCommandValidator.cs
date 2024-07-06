using FluentValidation;
using NetChill.Domain.Constants;

namespace NetChill.Application.Features.Genre.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator() 
        {
            RuleFor(r => r.GenreName)
                .NotEmpty().WithMessage("Genre Name field is required.")
                .Matches(RegexExpressions.ProperCase).WithMessage("Genre name can only be 'Proper Case'.");

            RuleFor(r => r.GenreDescription)
                .NotEmpty().WithMessage("Genre Description field is required.")
                .MaximumLength(225).WithMessage("Genre Description must not exceed 255 characters.")
                .Matches(RegexExpressions.SentenceCase).WithMessage("Genre Description can only be in 'Sentence case'.");
        }
    }
}

using FluentValidation;
using NetChill.Domain.Constants;

namespace NetChill.Application.Features.Genre.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(r => r.GenreName)
                .NotEmpty().WithMessage("Genre Name field is required.")
                .Matches(RegexExpressions.ProperCase).WithMessage("Genre name can only be 'Proper Case'.");

            RuleFor(r => r.GenreDescription)
                .MaximumLength(225).WithMessage("Description must not exceed 255 characters.")
                .Matches(RegexExpressions.SentenceCase).WithMessage("Genre Description can only be in 'Sentence case'.");
        }
    }
}

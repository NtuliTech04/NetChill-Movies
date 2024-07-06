using FluentValidation;

namespace NetChill.Application.Features.Movie.Production.Commands.CreateProducuction
{
    public class CreateMovieProductionCommandValidator : AbstractValidator<CreateMovieProductionCommand>
    {
        public CreateMovieProductionCommandValidator()
        {
            RuleFor(r => r.Directors)
                .NotEmpty().WithMessage("Directors field is required.");


            RuleFor(r => r.MovieRef)
               .NotEmpty().WithMessage("Movie referencing key is required.");
        }
    }
}

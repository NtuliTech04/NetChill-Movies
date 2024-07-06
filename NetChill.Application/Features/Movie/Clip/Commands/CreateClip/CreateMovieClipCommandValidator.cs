using FluentValidation;
using Microsoft.AspNetCore.Http;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Domain.Constants;

namespace NetChill.Application.Features.Movie.Clip.Commands.CreateClip
{
    public class CreateMovieClipCommandValidator : AbstractValidator<CreateMovieClipCommand>
    {
        public CreateMovieClipCommandValidator()
        {
            //Poster Image File
            RuleFor(r => r.MovieRef)
               .NotEmpty().WithMessage("Movie referencing key is required.");

            RuleFor(r => r.MoviePoster)
                .NotNull().WithMessage("Movie poster file is required.");

            RuleFor(r => r.MoviePoster.Length)
                .ExclusiveBetween(0, MediaConstants.ImageMaxBytes)
                .WithMessage($"Movie poster size cannot be more than {MediaConstants.ImageMaxMegaBytes} MB")
                .When(r => r.MoviePoster != null);

            RuleFor(r => r.MoviePoster.FileName)
                .Matches(RegexExpressions.ImageRegex)
                .WithMessage("Invalid image format. Only png and jpg are acceptable.");


            //Video Clip File

           When(r => !r.IsTrailer, () => 
           { 
                RuleFor(r => r.VideoClip.Length)
                    .ExclusiveBetween(0, MediaConstants.VideoMaxBytes)
                    .WithMessage($"Movie clip size cannot be more than {MediaConstants.VideoMaxMegaBytes} MB")
                    .When(r => r.VideoClip != null);

                RuleFor(r => r.VideoClip.FileName)
                    .Matches(RegexExpressions.VideoRegex)
                    .WithMessage("Invalid video format. Only mp4, webm, and oog are acceptable.");

            }).Otherwise(() =>
            {
                RuleFor(r => r.MovieTrailerUrl)
                    .NotNull().WithMessage("Movie trailer link is required.");
            });

        }
    }
}

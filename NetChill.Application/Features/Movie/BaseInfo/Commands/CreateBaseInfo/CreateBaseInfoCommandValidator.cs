using FluentValidation;
using NetChill.Application.Abstractions;
using NetChill.Domain.Constants;

namespace NetChill.Application.Features.Movie.BaseInfo.Commands.CreateInfo
{
    public class CreateBaseInfoCommandValidator : AbstractValidator<CreateBaseInfoCommand>
    {
        public CreateBaseInfoCommandValidator(IDateTimeService date)
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("Movie Title field is required.")
                .MaximumLength(225).WithMessage("Title must not exceed 255 characters.")
                .Matches(RegexExpressions.TitleNumericCase)
                .WithMessage("Movie Title can only be in 'Proper Case' and/or numerics.");

            RuleFor(r => r.Genre)
                .NotEmpty().WithMessage("Select at least one movie genre.");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Movie Description field is required.")
                .MaximumLength(8000).WithMessage("Description must not exceed 8000 characters.")
                .Matches(RegexExpressions.SentenceCase)
                .WithMessage("Movie Description can only be in 'Sentence case'.");

            RuleFor(r => r.Languages)
                .NotEmpty().WithMessage("Select at least one language spoken in this movie.");

            RuleFor(r => r.YearReleased)
                .NotEmpty().WithMessage("Select the year this movie was released.")
                .LessThanOrEqualTo(date.NowUtc.Year).WithMessage("Year of release cannot be greater than this year.");

            RuleFor(r => r.AvailableFrom)
                .NotEmpty().WithMessage("Select the date this movie will be available.")
                .GreaterThanOrEqualTo(date.NowUtc.Date).WithMessage("Availability date cannot be less than today's date.");
        }
    }
}

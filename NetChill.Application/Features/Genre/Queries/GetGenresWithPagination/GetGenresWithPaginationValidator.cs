using FluentValidation;

namespace NetChill.Application.Features.Genre.Queries.GetGenresWithPagination
{
    public class GetGenresWithPaginationValidator : AbstractValidator<GetGenresWithPaginationQuery>
    {
        public GetGenresWithPaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page size must be greater than or equal to 1.");
        }
    }
}

using FluentValidation;

namespace NetChill.Application.Features.Language.Queries.GetLanguageWithPagination
{
    public class GetLanguagesWithPaginationValidator : AbstractValidator<GetLanguagesWithPaginationQuery>
    {
        public GetLanguagesWithPaginationValidator()
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

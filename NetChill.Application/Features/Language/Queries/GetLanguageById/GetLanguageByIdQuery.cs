using AutoMapper;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Features.Genre.Queries.GetGenreById;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Language.Queries.GetLanguageById
{
    public record GetLanguageByIdQuery : IRequest<Result<GetLanguageByIdDto>>
    {
        public int LanguageId { get; set; }

        public GetLanguageByIdQuery()
        {
            
        }

        public GetLanguageByIdQuery(int languageId)
        {
            LanguageId = languageId;
        }
    }

    internal class GetLanguageByIdQueryHandler : IRequestHandler<GetLanguageByIdQuery, Result<GetLanguageByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLanguageByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetLanguageByIdDto>> Handle(GetLanguageByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<MovieLanguage>().GetByIdAsync(query.LanguageId);

            if (entity != null)
            {
                try
                {
                    var language = _mapper.Map<GetLanguageByIdDto>(entity);
                    return await Result<GetLanguageByIdDto>.SuccessAsync(language, "Language Retrieved Successfully.");
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ResponseConstants.Error520, ex);
                }
            }
            else
            {
                return await Result<GetLanguageByIdDto>.FailureAsync("Language Not Found.");
            }
        }
    }
}

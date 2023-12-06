using AutoMapper;
using MediatR;
using NetChill.Application.Interfaces.Repositories;
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
            var entity = await _unitOfWork.Repository<MovieLanguage>().GetByIntIdAsync(query.LanguageId);
            var language = _mapper.Map<GetLanguageByIdDto>(entity);
            return await Result<GetLanguageByIdDto>.SuccessAsync(language);
        }
    }
}

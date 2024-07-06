using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Language.Queries.GetAllLanguages
{
    public record GetAllLanguagesQuery : IRequest<Result<List<GetAllLanguagesDto>>>;

    internal class GetAllLanguagesHandler : IRequestHandler<GetAllLanguagesQuery, Result<List<GetAllLanguagesDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllLanguagesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public  async Task<Result<List<GetAllLanguagesDto>>> Handle(GetAllLanguagesQuery query, CancellationToken cancellationToken)
        {
            var languages = await _unitOfWork.Repository<MovieLanguage>().Entities
                .ProjectTo<GetAllLanguagesDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<GetAllLanguagesDto>>.SuccessAsync(languages, "Languages Retrieved Successfully.");
        }
    }
}



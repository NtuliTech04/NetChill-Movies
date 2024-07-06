using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllFeaturedMoviesInfo
{
    public record GetAllFeaturedMoviesInfoQuery : IRequest<Result<List<GetAllFeaturedMoviesInfoDto>>>;

    internal class GetAllFeaturedMoviesInfoHandler : IRequestHandler<GetAllFeaturedMoviesInfoQuery, Result<List<GetAllFeaturedMoviesInfoDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllFeaturedMoviesInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllFeaturedMoviesInfoDto>>> Handle(GetAllFeaturedMoviesInfoQuery query, CancellationToken cancellationToken)
        {
            var featured = await _unitOfWork.Repository<MovieBaseInfo>().Entities
                .Where(x=>x.IsFeatured == true && x.IsUpcoming == false)
                .ProjectTo<GetAllFeaturedMoviesInfoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<GetAllFeaturedMoviesInfoDto>>.SuccessAsync(featured);
        }
    }
}

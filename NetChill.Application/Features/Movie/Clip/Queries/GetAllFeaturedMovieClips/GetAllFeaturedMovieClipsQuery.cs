using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.Clip.Queries.GetAllFeaturedMovieClips
{
    public record GetAllFeaturedMovieClipsQuery : IRequest<Result<List<GetAllFeaturedMovieClipsDto>>>;

    internal class GetAllFeaturedMovieClipsHandler : IRequestHandler<GetAllFeaturedMovieClipsQuery, Result<List<GetAllFeaturedMovieClipsDto>>> 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllFeaturedMovieClipsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllFeaturedMovieClipsDto>>> Handle(GetAllFeaturedMovieClipsQuery query, CancellationToken cancellationToken)
        {
            var featured = await _unitOfWork.Repository<MovieClip>().Entities
                .Where(x => x.MovieBaseInfo.IsFeatured == true && x.MovieBaseInfo.IsUpcoming == false)
                .ProjectTo<GetAllFeaturedMovieClipsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<GetAllFeaturedMovieClipsDto>>.SuccessAsync(featured);
        }
    }
}

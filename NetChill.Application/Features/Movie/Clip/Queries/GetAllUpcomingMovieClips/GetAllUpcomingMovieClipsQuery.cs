using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Interfaces;
using NetChill.Application.Interfaces.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.Clip.Queries.GetAllUpcomingMovieClips
{
    public record GetAllUpcomingMovieClipsQuery : IRequest<Result<List<GetAllUpcomingMovieClipsDto>>>;

    internal class GetAllUpcomingMovieClipsHandler : IRequestHandler<GetAllUpcomingMovieClipsQuery, Result<List<GetAllUpcomingMovieClipsDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _date;

        public GetAllUpcomingMovieClipsHandler(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService date)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _date = date;
        }

        public async Task<Result<List<GetAllUpcomingMovieClipsDto>>> Handle(GetAllUpcomingMovieClipsQuery query, CancellationToken cancellationToken)
        {
            var upcoming = await _unitOfWork.Repository<MovieClip>().Entities
                .Where(x => x.MovieBaseInfo.AvailableFrom > _date.NowUtc)
                .ProjectTo<GetAllUpcomingMovieClipsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<GetAllUpcomingMovieClipsDto>>.SuccessAsync(upcoming);
        }
    }
}

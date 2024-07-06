using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.Clip.Queries.GetAllLatestMovieClips
{
    public record GetAllLatestMovieClipsQuery : IRequest<Result<List<GetAllLatestMovieClipsDto>>>;
    
    internal class GetAllLatestMovieClipsHandler : IRequestHandler<GetAllLatestMovieClipsQuery, Result<List<GetAllLatestMovieClipsDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _date;

        public GetAllLatestMovieClipsHandler(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService date)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _date = date;
        }

        public async Task<Result<List<GetAllLatestMovieClipsDto>>> Handle(GetAllLatestMovieClipsQuery query, CancellationToken cancellationToken)
        {
            var latest = await _unitOfWork.Repository<MovieClip>().Entities
                .Where(x => x.MovieBaseInfo.IsUpcoming == false && x.MovieBaseInfo.IsFeatured == false)
                .Where(x => x.MovieBaseInfo.YearReleased == _date.NowUtc.Year || x.MovieBaseInfo.YearReleased == _date.NowUtc.Year - 1)
                .ProjectTo<GetAllLatestMovieClipsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<GetAllLatestMovieClipsDto>>.SuccessAsync(latest);
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Interfaces;
using NetChill.Application.Interfaces.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllLatestMoviesInfo
{
    public record GetAllLatestMoviesInfoQuery : IRequest<Result<List<GetAllLatestMoviesInfoDto>>>;

    internal class GetAllLatestMoviesInfoHandler : IRequestHandler<GetAllLatestMoviesInfoQuery, Result<List<GetAllLatestMoviesInfoDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _date;

        public GetAllLatestMoviesInfoHandler(IUnitOfWork unitOfWork, IMapper mapper, IDateTimeService date)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _date = date;
        }

        public async Task<Result<List<GetAllLatestMoviesInfoDto>>> Handle(GetAllLatestMoviesInfoQuery query, CancellationToken cancellationToken)
        {
            var latest = await _unitOfWork.Repository<MovieBaseInfo>().Entities
                .Where(x =>
                            x.AvailableFrom <= _date.NowUtc &&
                            x.IsFeatured == false &&
                            x.YearReleased == _date.NowUtc.Year ||
                            x.YearReleased == _date.NowUtc.Year - 1
                ).ProjectTo<GetAllLatestMoviesInfoDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

            return await Result<List<GetAllLatestMoviesInfoDto>>.SuccessAsync(latest);
        }
    }
}

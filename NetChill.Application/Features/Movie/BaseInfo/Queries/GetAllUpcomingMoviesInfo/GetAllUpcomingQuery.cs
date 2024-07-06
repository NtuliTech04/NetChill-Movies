using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllUpcoming
{
    public record GetAllUpcomingQuery : IRequest<Result<List<GetAllUpcomingDto>>>;

    internal class GetAllUpcomingHandler : IRequestHandler<GetAllUpcomingQuery, Result<List<GetAllUpcomingDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _date;

        public GetAllUpcomingHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllUpcomingDto>>> Handle(GetAllUpcomingQuery query, CancellationToken cancellationToken)
        {
            var upcoming = await _unitOfWork.Repository<MovieBaseInfo>().Entities
                .Where(x => x.IsUpcoming == true)
                .ProjectTo<GetAllUpcomingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<GetAllUpcomingDto>>.SuccessAsync(upcoming);
        }
    }
}

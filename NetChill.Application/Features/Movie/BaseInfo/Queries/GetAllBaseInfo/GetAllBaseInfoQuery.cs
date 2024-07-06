using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllBaseInfo
{
    public record GetAllBaseInfoQuery : IRequest<Result<List<GetAllBaseInfoDto>>>;

    internal class GetAllBaseInfoHandler : IRequestHandler<GetAllBaseInfoQuery, Result<List<GetAllBaseInfoDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBaseInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllBaseInfoDto>>> Handle(GetAllBaseInfoQuery query, CancellationToken cancellationToken)
        {
            var movies = await _unitOfWork.Repository<MovieBaseInfo>().Entities
                .ProjectTo<GetAllBaseInfoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<GetAllBaseInfoDto>>.SuccessAsync(movies, "Movie BaseInfoes Retrieved Successfully.");
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Interfaces.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.Production.Queries.GetAllProduction
{
    public record GetAllProductionQuery : IRequest<Result<List<GetAllProductionDto>>>;

    internal class GetAllProductionHandler : IRequestHandler<GetAllProductionQuery, Result<List<GetAllProductionDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllProductionDto>>> Handle(GetAllProductionQuery query, CancellationToken cancellationToken)
        {
            var productions = await _unitOfWork.Repository<MovieProduction>().Entities
                .ProjectTo<GetAllProductionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<GetAllProductionDto>>.SuccessAsync(productions, "Movie Productions Retrieved Successfully.");
        }
    }
}

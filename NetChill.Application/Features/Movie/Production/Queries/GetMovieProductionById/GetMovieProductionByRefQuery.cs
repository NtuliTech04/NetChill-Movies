using AutoMapper;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Abstractions.Repositories.Movie;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.Production.Queries.GetMovieProductionById
{
    public record GetMovieProductionByRefQuery : IRequest<Result<GetMovieProductionByRefDto>>
    {
        public Guid MovieRef { get; set; }

        public GetMovieProductionByRefQuery()
        {
            
        }

        public GetMovieProductionByRefQuery(Guid movieRef)
        {
            MovieRef = movieRef;
        }
    }

    internal class GetMovieProductionByIdQueryHandler : IRequestHandler<GetMovieProductionByRefQuery, Result<GetMovieProductionByRefDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMovieProductionRepository _productionRepository;

        public GetMovieProductionByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMovieProductionRepository productionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productionRepository = productionRepository;
        }

        public async Task<Result<GetMovieProductionByRefDto>> Handle (GetMovieProductionByRefQuery query, CancellationToken cancellationToken)
        {

            var entity = await _productionRepository.GetProductionByMovieRef(query.MovieRef);

            if (entity != null)
            {
                try
                {
                    var movie = _mapper.Map<GetMovieProductionByRefDto>(entity);
                    return await Result<GetMovieProductionByRefDto>.SuccessAsync(movie, "Movie Production Retrieved Successfully.");
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ResponseConstants.Error520, ex);
                }
            }
            else
            {
                return await Result<GetMovieProductionByRefDto>.FailureAsync("Movie Production Not Found.");
            }
        }
    }
}

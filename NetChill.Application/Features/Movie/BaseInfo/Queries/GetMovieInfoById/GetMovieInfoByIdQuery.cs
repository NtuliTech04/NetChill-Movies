using AutoMapper;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.BaseInfo.Queries.GetMovieInfoById
{
    public record GetMovieInfoByIdQuery : IRequest<Result<GetMovieInfoByIdDto>>
    {
        public Guid MovieId { get; set; }

        public GetMovieInfoByIdQuery()
        {
            
        }

        public GetMovieInfoByIdQuery(Guid movieId)
        {
            MovieId = movieId;   
        }
    }

    internal class GetMovieByIdQueryHandler: IRequestHandler<GetMovieInfoByIdQuery, Result<GetMovieInfoByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMovieByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetMovieInfoByIdDto>> Handle(GetMovieInfoByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<MovieBaseInfo>().GetByIdAsync(query.MovieId);
            
            if (entity != null)
            {
                try
                {
                    var movie = _mapper.Map<GetMovieInfoByIdDto>(entity);
                    return await Result<GetMovieInfoByIdDto>.SuccessAsync(movie, "Movie Retrieved Successfully.");
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ResponseConstants.Error520, ex);
                }
            }
            else
            {
                return await Result<GetMovieInfoByIdDto>.FailureAsync("Movie Not Found.");
            }
        }
    }
}

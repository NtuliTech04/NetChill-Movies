using AutoMapper;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Genre.Queries.GetGenreById
{
    public record GetGenreByIdQuery : IRequest<Result<GetGenreByIdDto>>
    {
        public int GenreId { get; set; }

        public GetGenreByIdQuery()
        {
            
        }

        public GetGenreByIdQuery(int genreId)
        {
            GenreId = genreId;
        }
    }

    internal class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, Result<GetGenreByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGenreByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetGenreByIdDto>> Handle(GetGenreByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<MovieGenre>().GetByIdAsync(query.GenreId);

            if (entity != null)
            {
                try
                {
                    var genre = _mapper.Map<GetGenreByIdDto>(entity);
                    return await Result<GetGenreByIdDto>.SuccessAsync(genre, "Genre Retrieved Successfully.");
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ResponseConstants.Error520, ex);
                }
            }
            else
            {
                return await Result<GetGenreByIdDto>.FailureAsync("Genre Not Found.");
            }
        }
    }
}

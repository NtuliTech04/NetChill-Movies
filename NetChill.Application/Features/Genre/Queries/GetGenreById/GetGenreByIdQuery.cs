using AutoMapper;
using MediatR;
using NetChill.Application.Interfaces.Repositories;
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
            var entity = await _unitOfWork.Repository<MovieGenre>().GetByIntIdAsync(query.GenreId);
            var genre = _mapper.Map<GetGenreByIdDto>(entity);
            return await Result<GetGenreByIdDto>.SuccessAsync(genre);
        }
    }
}

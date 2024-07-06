using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Genre.Queries.GetAllGenres
{
    public record GetAllGenresQuery : IRequest<Result<List<GetAllGenresDto>>>;

    internal class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, Result<List<GetAllGenresDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllGenresQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllGenresDto>>> Handle(GetAllGenresQuery query, CancellationToken cancellationToken)
        {
            var genres = await _unitOfWork.Repository<MovieGenre>().Entities
                .ProjectTo<GetAllGenresDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<GetAllGenresDto>>.SuccessAsync(genres, "Genres Retrieved Successfully.");
        }
    }
}

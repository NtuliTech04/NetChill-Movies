using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Abstractions.Repositories.Movie;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Persistence.Repositories.Movie
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        private readonly IGenericRepository<MovieGenre> _repository;

        public MovieGenreRepository(IGenericRepository<MovieGenre> repository)
        {
            _repository = repository;
        }
    }
}

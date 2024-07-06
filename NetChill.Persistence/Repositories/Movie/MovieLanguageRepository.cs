using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Abstractions.Repositories.Movie;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Persistence.Repositories.Movie
{
    public class MovieLanguageRepository : IMovieLanguageRepository
    {
        private readonly IGenericRepository<MovieLanguage> _repository;

        public MovieLanguageRepository(IGenericRepository<MovieLanguage> repository)
        {
            _repository = repository;   
        }
    }
}

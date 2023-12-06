using NetChill.Application.Interfaces.Repositories;
using NetChill.Application.Interfaces.Repositories.Movie;
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

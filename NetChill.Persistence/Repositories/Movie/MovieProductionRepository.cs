using NetChill.Application.Interfaces.Repositories;
using NetChill.Application.Interfaces.Repositories.Movie;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Persistence.Repositories.Movie
{
    public class MovieProductionRepository : IMovieProductionRepository
    {
        private readonly IGenericRepository<MovieLanguage> _repository;

        public MovieProductionRepository(IGenericRepository<MovieLanguage> repository)
        {
            _repository = repository;   
        }
    }
}

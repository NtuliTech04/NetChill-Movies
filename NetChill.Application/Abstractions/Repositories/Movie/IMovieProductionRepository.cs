using NetChill.Domain.Entities.Movie;

namespace NetChill.Application.Abstractions.Repositories.Movie
{
    //Implements other custom methods for additional functionality besides CRUD methods in IGenericRepository.
    public interface IMovieProductionRepository
    {
        Task<MovieProduction> GetProductionByMovieRef(Guid movieRef);
    }
}

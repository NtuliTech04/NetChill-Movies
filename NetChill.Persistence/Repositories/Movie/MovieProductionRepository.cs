using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Abstractions.Repositories.Movie;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Persistence.Repositories.Movie
{
    public class MovieProductionRepository : IMovieProductionRepository
    {
        private readonly IGenericRepository<MovieProduction> _productionRepository;

        public MovieProductionRepository(IGenericRepository<MovieProduction> repository)
        {
            _productionRepository = repository;   
        }


        //Get movie production by movie reference (fk)
        public async Task<MovieProduction> GetProductionByMovieRef(Guid movieRef)
        {
            return await _productionRepository.Entities
                   .FirstOrDefaultAsync(x =>
                   x.MovieRef == movieRef);
        }
    }
}

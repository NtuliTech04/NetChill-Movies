using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Abstractions.Repositories.Movie;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Persistence.Repositories.Movie
{
    public class MovieBaseInfoRepository : IMovieBaseInfoRepository
    {
        private readonly IGenericRepository<MovieBaseInfo> _repository;
        public MovieBaseInfoRepository(IGenericRepository<MovieBaseInfo> repository)
        {
            _repository = repository;   
        }


        //Custom methods defined within the respective repository as follows. 


        //Check movie existence by Id
        public async Task<bool> CheckMovieExistence(Guid Id)
        {
            var checkMovie = await _repository.GetByIdAsync(Id);

            if (checkMovie == null)
            {
                return false;
            }

            return true;
        }



        //Get movie title by Id
        public async Task<string> GetMovieTitle(Guid Id)
        {
            var movie =  await _repository
                        .Entities
                        .FirstOrDefaultAsync(x => x
                        .MovieId == Id);

            return movie?.Title;
        }
    }
}

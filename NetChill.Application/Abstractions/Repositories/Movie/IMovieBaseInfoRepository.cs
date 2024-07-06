namespace NetChill.Application.Abstractions.Repositories.Movie
{
    //Implements other custom methods for additional functionality besides CRUD methods in IGenericRepository.
    public interface IMovieBaseInfoRepository
    {
        Task<bool> CheckMovieExistence(Guid Id);
        Task<string> GetMovieTitle(Guid Id);
    }
}

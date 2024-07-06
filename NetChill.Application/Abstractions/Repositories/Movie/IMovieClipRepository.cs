using Microsoft.AspNetCore.Http;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Application.Abstractions.Repositories.Movie
{
    //Implements other custom methods for additional functionality besides CRUD methods in IGenericRepository.
    public interface IMovieClipRepository
    {
        Guid MovieRef { get; set; }
        Task<bool> CheckMovieRefExistence();
        Task<MovieClip> GetFilesByMovieRef(Guid movieRef);
        Task<string> UploadMoviePoster(IFormFile _iformFile);
        Task<string> UploadMovieClip(IFormFile _iformFile);
    }
}

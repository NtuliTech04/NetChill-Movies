using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace NetChill.Application.Interfaces.Repositories.Movie
{
    //Implements other custom methods for additional functionality besides CRUD methods in IGenericRepository.
    public interface IMovieClipRepository
    {
        Guid MovieRef { get; set; }
        Task<bool> CheckMovieRefExistence();
        Task<string> UploadMoviePoster(IFormFile _iformFile); 
        Task<string> UploadMovieClip(IFormFile _iformFile);
    }
}

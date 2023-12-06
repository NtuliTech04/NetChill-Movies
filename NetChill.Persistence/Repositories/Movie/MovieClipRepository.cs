using Microsoft.AspNetCore.Http;
using NetChill.Domain.Entities.Movie;
using NetChill.Application.Common.FileInfo;
using NetChill.Application.Interfaces.Repositories;
using NetChill.Application.Interfaces.Repositories.Movie;
using NetChill.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace NetChill.Persistence.Repositories.Movie
{
    public class MovieClipRepository : IMovieClipRepository
    {
        private readonly IGenericRepository<MovieClip> _clipRepository;
        private readonly IMovieBaseInfoRepository _infoRepository;


        public MovieClipRepository(IMovieBaseInfoRepository infoRepository, IGenericRepository<MovieClip> clipRepository)
        {
            _infoRepository = infoRepository;
            _clipRepository = clipRepository;
        }


        //Custom methods defined within the respective repository as follows. 


        public Guid MovieRef { get; set; } //Gets and sets the movie key to use within the uploading methods


        //Checks whether the MovieClip object has already been reference by the same movie (MovieRef) or not
        public async Task<bool> CheckMovieRefExistence()
        {
            var isExist = await _clipRepository.Entities.FirstOrDefaultAsync(x => x.MovieRef == MovieRef);

            if (isExist != null)
            {
                return true;
            }

            return false;
        }


        public async Task<string> UploadMoviePoster(IFormFile iformFile)
        {
            try
            {

                if (await _infoRepository.CheckMovieExistence(MovieRef) == false)
                {
                    throw new NotFoundException("This movie does not exist.");
                }
                else if (await CheckMovieRefExistence() == true)
                {
                    throw new BadRequestException("Media files for this movie already exist.");
                }

                FileInfo fileInfo = new (iformFile.FileName);

                //Gets and store title in a variable for dir naming
                string MovieTitle = await _infoRepository.GetMovieTitle(MovieRef);

                //Generates poster name from movie title...
                var posterName = MovieTitle + "_" + DateTime.Now.Ticks.ToString() + fileInfo.Extension;

                //Passes movie title to create a dir specific to the movie and gets the full poster path
                var fullPosterPath = FileDetails.GetFullPath(posterName, MovieTitle);

                //Passes movie title, gets the local poster file and save to the database
                var localPosterPath = FileDetails.GetLocalPath(posterName, MovieTitle);

                //Uploads files to the newly created dir
                using (var fileStream = new FileStream(fullPosterPath, FileMode.Create))
                {
                    await iformFile.CopyToAsync(fileStream);
                }

                return localPosterPath;
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"{ex}");
            }
        }


        public async Task<string> UploadMovieClip(IFormFile iformFile)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(iformFile.FileName);

                //Gets and store title in a variable
                string MovieTitle = await _infoRepository.GetMovieTitle(MovieRef);

                //Generates clip name from movie title...
                var clipName = MovieTitle + "_" + DateTime.Now.Ticks.ToString() + fileInfo.Extension;

                //Passes movie title to create a dir specific to the movie and gets the full clip path
                var getClipPath = FileDetails.GetFullPath(clipName, MovieTitle);

                //Passes movie title, gets the local clip file and save to the database
                var localClipPath = FileDetails.GetLocalPath(clipName, MovieTitle);


                //Uploads files to the newly created dir
                using (var fileStream = new FileStream(getClipPath, FileMode.Create))
                {
                    await iformFile.CopyToAsync(fileStream);
                }

                return localClipPath;
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"{ex}");
            }
        }
    }
}

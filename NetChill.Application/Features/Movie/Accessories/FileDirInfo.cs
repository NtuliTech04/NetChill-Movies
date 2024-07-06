namespace NetChill.Application.Features.Movie.Accessories
{
    public static class FileDirInfo
    {
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        //Uses movie title to create a dir specific to the movie and save filestream
        public static string GetContentDirectoryFullPath(string movieTitle)
        {
            var result = Path.Combine(GetCurrentDirectory(), $"Media Files\\{movieTitle}\\");
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }
            return result;
        }


        //Uses movie title to create a local path to save to the database
        public static string GetContentDirectoryLocalPath(string movieTitle)
        {
            var result = Path.Combine($"Media Files\\{movieTitle}\\");
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }
            return result;
        }


        //Gets the full file path from the computer's drive
        public static string GetFullPath(string FileName, string movieTitle)
        {
            var fullContentDir = GetContentDirectoryFullPath(movieTitle);
            return Path.Combine(fullContentDir, FileName);
        }


        //Gets the local path within application files
        public static string GetLocalPath(string FileName, string movieTitle)
        {
            var serverContentDir = GetContentDirectoryLocalPath(movieTitle);
            return Path.Combine(serverContentDir, FileName);
        }
    }
}

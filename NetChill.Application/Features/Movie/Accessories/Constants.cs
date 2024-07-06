namespace NetChill.Application.Features.Movie.Accessories
{
    public class ResponseConstants
    {
        //Unknown Error (520)
        public const string Error520 = "Something Went Wrong!...";

        //Referencing a movie that has already been referenced or doesn't exist at all. 
        #region Error Message

        const string movieExistOrNull = "Saving attempt was unsuccessful. Either this entity has already " +
                                        "been referenced with the same MovieRef which violates the rule of a " +
                                        "one-to-one relationship between the MovieBaseInfo entity and this entity, " +
                                        "or the referenced movie does not exist at all. See Exception: ";

        #endregion
        public const string MovieExistOrNull = movieExistOrNull;
    }
    

    public class MediaConstants
    {
        //Max size for images
        public const int ImageMaxBytes = 2097152;    //2097152 Bytes - 2 MegaBytes
        public const int ImageMaxMegaBytes = 2097152 / ToMegabytes;

        //Max size for videos
        public const int VideoMaxBytes = 26214400;   //26214400 Bytes - 25 MegaBytes
        public const int VideoMaxMegaBytes = 26214400 / ToMegabytes;

        //Value to convert bytes to megabytes
        public const int ToMegabytes = 1048576;
    }
}

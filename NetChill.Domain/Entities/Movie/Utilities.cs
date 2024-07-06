using NetChill.Domain.Common;

namespace NetChill.Domain.Entities.Movie
{
    public class MovieGenre : BaseAuditableEntity
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string GenreDescription { get; set; }
    }



    public class MovieLanguage : BaseAuditableEntity
    {
        public int LanguageId { get; set; }
        public string SpokenLanguage { get; set; }
        public string LanguageNotes { get; set; }
    }



    public class TrackCreationProgress : BaseAuditableEntity
    {
        public int Id { get; set; }
        public double Progress { get; set; }
        public string Status { get; set; }

        public Guid MovieRef { get; set; }
        public MovieBaseInfo MovieBaseInfo { get; set; }
    }
}

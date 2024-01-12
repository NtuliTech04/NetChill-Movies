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
}

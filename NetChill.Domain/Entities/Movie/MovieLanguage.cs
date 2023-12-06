using NetChill.Domain.Common;

namespace NetChill.Domain.Entities.Movie
{
    public class MovieLanguage : BaseAuditableEntity
    {
        public int LanguageId { get; set; }
        public string SpokenLanguage { get; set; }
        public string LanguageNotes { get; set; }
    }
}

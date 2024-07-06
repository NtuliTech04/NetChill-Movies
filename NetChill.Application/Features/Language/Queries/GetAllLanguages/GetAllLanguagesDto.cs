using NetChill.Application.Common.Mappings;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Application.Features.Language.Queries.GetAllLanguages
{
    public class GetAllLanguagesDto : IMapFrom<MovieLanguage>
    {
        public int LanguageId { get; set; }
        public string SpokenLanguage { get; set; }
        public string LanguageNotes { get; set; }
    }
}

using NetChill.Application.Common.Mappings;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Application.Features.Language.Queries.GetLanguageById
{
    public class GetLanguageByIdDto : IMapFrom<MovieLanguage>
    {
        public int LanguageId { get; set; }
        public string SpokenLanguage { get; set; }
        public string LanguageNotes { get; set; }
    }
}

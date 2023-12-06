using NetChill.Domain.Common;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Domain.Events.Language
{
    public class LanguageDeletedEvent : BaseEvent
    {
        public MovieLanguage Language { get; }

        public LanguageDeletedEvent(MovieLanguage language)
        {
            Language = language;
        }
    }
}

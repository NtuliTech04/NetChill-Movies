using NetChill.Domain.Common;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Domain.Events.Language
{
    public class LanguageUpdatedEvent : BaseEvent
    {
        public MovieLanguage Language { get; }

        public LanguageUpdatedEvent(MovieLanguage language)
        {
            Language = language;
        }
    }
}

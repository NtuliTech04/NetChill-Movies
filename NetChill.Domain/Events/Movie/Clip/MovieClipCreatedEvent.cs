using NetChill.Domain.Common;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Domain.Events.Movie.Clip
{
    public class MovieClipCreatedEvent : BaseEvent
    {
        public MovieClip MovieClip { get; }

        public MovieClipCreatedEvent(MovieClip clip)
        {
            MovieClip = clip;
        }
    }
}

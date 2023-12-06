using NetChill.Domain.Common;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Domain.Events.Movie.BaseInfo
{
    public class MovieBaseCreatedEvent : BaseEvent
    {
        public MovieBaseInfo MovieBaseInfo { get; }

        public MovieBaseCreatedEvent(MovieBaseInfo baseInfo)
        {
            MovieBaseInfo = baseInfo;
        }
    }
}

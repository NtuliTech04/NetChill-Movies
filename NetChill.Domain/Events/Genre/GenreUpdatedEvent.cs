using NetChill.Domain.Common;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Domain.Events.Genre
{
    public class GenreUpdatedEvent : BaseEvent
    {
        public MovieGenre Genre { get; }

        public GenreUpdatedEvent(MovieGenre genre)
        {
            Genre = genre;
        }
    }
}

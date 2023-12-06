using NetChill.Domain.Common;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Domain.Events.Genre
{
    public class GenreCreatedEvent : BaseEvent
    {
        public MovieGenre Genre { get; }

        public GenreCreatedEvent(MovieGenre movieGenre)
        {
            Genre = movieGenre;
        }
    }
}

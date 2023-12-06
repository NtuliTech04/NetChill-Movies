using NetChill.Domain.Common;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Domain.Events.Genre
{
    public class GenreDeletedEvent : BaseEvent
    {
        public MovieGenre Genre { get; }

        public GenreDeletedEvent(MovieGenre genre)
        {
            Genre = genre;
        }
    }
}

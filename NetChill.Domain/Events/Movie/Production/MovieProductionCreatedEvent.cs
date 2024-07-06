using NetChill.Domain.Common;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Domain.Events.Movie.Production
{
    public class MovieProductionCreatedEvent :BaseEvent
    {
        public MovieProduction MovieProduction { get; }

        public MovieProductionCreatedEvent(MovieProduction production)
        {
            MovieProduction = production;
        }
    }
}

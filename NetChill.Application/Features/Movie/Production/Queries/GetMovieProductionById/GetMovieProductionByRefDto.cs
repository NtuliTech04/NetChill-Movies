using NetChill.Application.Common.Mappings;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Application.Features.Movie.Production.Queries.GetMovieProductionById
{
    public class GetMovieProductionByRefDto : IMapFrom<MovieProduction>
    {
        public int ProductionId { get; set; }
        public string Directors { get; set; }
        public string Writers { get; set; }
        public string MovieStars { get; set; }

        public Guid MovieRef { get; set; }
    }
}

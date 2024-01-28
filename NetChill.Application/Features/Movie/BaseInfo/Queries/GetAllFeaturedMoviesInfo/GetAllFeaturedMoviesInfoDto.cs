using NetChill.Application.Common.Mappings;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllFeaturedMoviesInfo
{
    public class GetAllFeaturedMoviesInfoDto : IMapFrom<MovieBaseInfo>
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Languages { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsUpcoming { get; set; }
        public int YearReleased { get; set; }
        public DateTime AvailableFrom { get; set; }
        public double AvgRating { get; set; }
    }
}


using NetChill.Domain.Common;

namespace NetChill.Domain.Entities.Movie
{
    public class MovieBaseInfo : BaseAuditableEntity
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Languages { get; set; }
        public bool IsFeatured { get; set; }
        public int YearReleased { get; set; }
        public DateTime AvailableFrom { get; set; }
        public double? AvgRating { get; set; }


        public MovieProduction MovieProduction { get; set; }
        public MovieClip MovieClip { get; set; }
        public TrackCreationProgress TrackCreationProgress { get; set; }

    }
}

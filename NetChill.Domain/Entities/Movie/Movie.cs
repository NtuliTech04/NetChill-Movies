using NetChill.Domain.Common;

//nuget\packages\microsoft.aspnetcore.http.features\5.0.17

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
        public bool IsUpcoming { get; set; }
        public int YearReleased { get; set; }
        public DateTime AvailableFrom { get; set; }
        public double? AvgRating { get; set; }

        public MovieProduction MovieProduction { get; set; }
        public MovieClip MovieClip { get; set; }
        public TrackCreationProgress TrackCreationProgress { get; set; }
    }



    public class MovieProduction : BaseAuditableEntity
    {
        public int ProductionId { get; set; }
        public string Directors { get; set; }
        public string? Writers { get; set; }
        public string? MovieStars { get; set; }

        public Guid MovieRef { get; set; }
        public MovieBaseInfo MovieBaseInfo { get; set; }
    }



    public class MovieClip : BaseAuditableEntity
    {
        public int ClipId { get; set; }
        public string MoviePosterPath { get; set; }
        public string? VideoClipPath { get; set; }
        public string? MovieTrailerUrl { get; set; }
        public DateTime UploadDate { get; set; }

        public Guid MovieRef { get; set; }
        public MovieBaseInfo MovieBaseInfo { get; set; }
    }
}

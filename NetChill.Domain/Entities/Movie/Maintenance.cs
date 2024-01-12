using NetChill.Domain.Common;

namespace NetChill.Domain.Entities.Movie
{
    public class TrackCreationProgress : BaseAuditableEntity
    {
        public int Id { get; set; }
        public double Progress { get; set; }
        public string Status { get; set; }

        public Guid MovieRef { get; set; }
        public MovieBaseInfo MovieBaseInfo { get; set; }
    }
}

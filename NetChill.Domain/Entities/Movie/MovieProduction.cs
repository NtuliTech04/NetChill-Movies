using NetChill.Domain.Common;

namespace NetChill.Domain.Entities.Movie
{
    public class MovieProduction : BaseAuditableEntity
    {
        public int ProductionId { get; set; }
        public string Directors { get; set; }
        public string? Writers { get; set; }
        public string? MovieStars { get; set; }

        public Guid MovieRef { get; set; }
        public MovieBaseInfo MovieBaseInfo { get; set; }
    }
}

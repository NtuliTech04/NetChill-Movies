using NetChill.Application.Common.Mappings;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Application.Features.Movie.Clip.Queries.GetAllLatestMovieClips
{
    public class GetAllLatestMovieClipsDto : IMapFrom<MovieClip>
    {
        public int ClipId { get; set; }
        public string MoviePosterPath { get; set; }
        public string VideoClipPath { get; set; }
        public string MovieTrailerUrl { get; set; }
        public DateTime UploadDate { get; set; }

        public Guid MovieRef { get; set; }
    }
}

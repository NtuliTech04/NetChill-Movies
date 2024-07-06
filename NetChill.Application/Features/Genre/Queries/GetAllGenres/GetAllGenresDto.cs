using NetChill.Application.Common.Mappings;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Application.Features.Genre.Queries.GetAllGenres
{
    public class GetAllGenresDto : IMapFrom<MovieGenre>
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string GenreDescription { get; set; }
    }
}

﻿using NetChill.Application.Common.Mappings;
using NetChill.Domain.Entities.Movie;

namespace NetChill.Application.Features.Genre.Queries.GetGenresWithPagination
{
    public class GetGenresWithPaginationDto : IMapFrom<MovieGenre>
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string GenreDescription { get; set; }
    }
}

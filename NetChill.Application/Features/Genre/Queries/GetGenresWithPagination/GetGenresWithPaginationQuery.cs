using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Extensions;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Genre.Queries.GetGenresWithPagination
{
    //Not Yet Implemented In Genres Controller
    public record GetGenresWithPaginationQuery : IRequest<PaginatedResult<GetGenresWithPaginationDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetGenresWithPaginationQuery()
        {
            
        }

        public GetGenresWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        internal class GetGenresWithPaginationQueryHandler : IRequestHandler<GetGenresWithPaginationQuery, PaginatedResult<GetGenresWithPaginationDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetGenresWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<PaginatedResult<GetGenresWithPaginationDto>> Handle(GetGenresWithPaginationQuery query, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Repository<MovieGenre>()
                    .Entities.OrderBy(x => x.GenreName)
                    .ProjectTo<GetGenresWithPaginationDto>(_mapper.ConfigurationProvider)
                    .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
            }
        }
    }
}

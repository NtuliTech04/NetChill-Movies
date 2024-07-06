using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Extensions;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Language.Queries.GetLanguageWithPagination
{
    //Not Yet Implemented In Languages Controller
    public record GetLanguagesWithPaginationQuery : IRequest<PaginatedResult<GetLanguagesWithPaginationDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetLanguagesWithPaginationQuery()
        {
            
        }

        public GetLanguagesWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        internal class GetLanguagesWithPaginationQueryHandler : IRequestHandler<GetLanguagesWithPaginationQuery, PaginatedResult<GetLanguagesWithPaginationDto>> 
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetLanguagesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<PaginatedResult<GetLanguagesWithPaginationDto>> Handle(GetLanguagesWithPaginationQuery query, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Repository<MovieLanguage>()
                    .Entities.OrderBy(x=>x.SpokenLanguage)
                    .ProjectTo<GetLanguagesWithPaginationDto>(_mapper.ConfigurationProvider)
                    .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
            }
        }
    }
}

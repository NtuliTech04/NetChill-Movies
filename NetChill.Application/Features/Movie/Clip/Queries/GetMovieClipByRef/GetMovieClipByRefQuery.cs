using AutoMapper;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Abstractions.Repositories.Movie;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.Clip.Queries.GetMovieClipByRef
{
    public record GetMovieClipByRefQuery : IRequest<Result<GetMovieClipByRefDto>> 
    {
        public Guid MovieRef { get; set; }

        public GetMovieClipByRefQuery()
        {
            
        }

        public GetMovieClipByRefQuery(Guid movieRef)
        {
            MovieRef = movieRef;
        }
    }

    internal class GetMovieClipByRefQueryHandler: IRequestHandler<GetMovieClipByRefQuery, Result<GetMovieClipByRefDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMovieClipRepository _clipRepository;

        public GetMovieClipByRefQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMovieClipRepository clipRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clipRepository = clipRepository;
        }

        public async Task<Result<GetMovieClipByRefDto>> Handle(GetMovieClipByRefQuery query, CancellationToken cancellationToken)
        {
            var entity = await _clipRepository.GetFilesByMovieRef(query.MovieRef);

            if (entity != null)
            {
                try
                {
                    var movie = _mapper.Map<GetMovieClipByRefDto>(entity);
                    return await Result<GetMovieClipByRefDto>.SuccessAsync(movie, "Movie Files Retrieved Successfully.");
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ResponseConstants.Error520, ex);
                }
            }
            else
            {
                return await Result<GetMovieClipByRefDto>.FailureAsync("Movie Files Not Found.");
            }
        }
    }
}

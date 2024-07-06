using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.Clip.Queries.GetAllMovieClips
{
    public record class GetAllMovieClipsQuery : IRequest<Result<List<GetAllMovieClipsDto>>>;
    
    internal class GetAllMovieClipsHandler : IRequestHandler<GetAllMovieClipsQuery, Result<List<GetAllMovieClipsDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMovieClipsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllMovieClipsDto>>> Handle(GetAllMovieClipsQuery query, CancellationToken cancellationToken)
        {
            var clips = await _unitOfWork.Repository<MovieClip>().Entities
                .ProjectTo<GetAllMovieClipsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<GetAllMovieClipsDto>>.SuccessAsync(clips, "Movie Clips Retrieved Successfully.");
        }
    }
}

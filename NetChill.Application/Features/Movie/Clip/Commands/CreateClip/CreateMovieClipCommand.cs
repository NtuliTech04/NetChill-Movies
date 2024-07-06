using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Abstractions.Repositories.Movie;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Common.Mappings;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Domain.Entities.Movie;
using NetChill.Domain.Events.Movie.Clip;
using NetChill.Shared;

namespace NetChill.Application.Features.Movie.Clip.Commands.CreateClip
{
    public record CreateMovieClipCommand : IRequest<Result<Guid>>, IMapFrom<MovieClip>
    {
        public Guid MovieRef { get; set; }

        public IFormFile MoviePoster { get; set; }

        public IFormFile? VideoClip { get; set; }

        public string? MovieTrailerUrl { get; set; }

        public bool IsTrailer => VideoClip == null;
    }

    internal class CreateMovieClipCommandHandler : IRequestHandler<CreateMovieClipCommand, Result<Guid>>
    {
        private readonly ITrackCreationProgressRepository _tracker;
        private readonly IMovieClipRepository _movieClipRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMovieClipCommandHandler(ITrackCreationProgressRepository tracker, IMovieClipRepository movieClipRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _movieClipRepository = movieClipRepository;
            _unitOfWork = unitOfWork;
            _tracker = tracker;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateMovieClipCommand command, CancellationToken cancellationToken)
        {
            try
            {
                //Gets the movie key to use within the uploading methods
                 _movieClipRepository.MovieRef = command.MovieRef;


                var clip = new MovieClip()
                {
                    MovieRef = command.MovieRef,
                    MoviePosterPath = await _movieClipRepository.UploadMoviePoster(command.MoviePoster),
                    VideoClipPath = await _movieClipRepository.UploadMovieClip(command.VideoClip),
                    MovieTrailerUrl = command.MovieTrailerUrl,
                    UploadDate = DateTime.UtcNow
                };

                await _unitOfWork.Repository<MovieClip>().InsertAsync(clip);
                clip.AddDomainEvent(new MovieClipCreatedEvent(clip));

                await _tracker.UpdateTracker(clip.MovieRef);
                await _unitOfWork.Save(cancellationToken);

                return await Result<Guid>.SuccessAsync(clip.MovieRef, "MovieClip Created Successfully.");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ResponseConstants.MovieExistOrNull, ex);
            }
        }
    }
}

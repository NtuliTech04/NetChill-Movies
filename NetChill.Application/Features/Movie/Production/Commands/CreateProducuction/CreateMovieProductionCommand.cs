using MediatR;
using AutoMapper;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Common.Mappings;
using NetChill.Domain.Entities.Movie;
using NetChill.Domain.Events.Movie.Production;
using NetChill.Shared;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Abstractions.Repositories.Movie;

namespace NetChill.Application.Features.Movie.Production.Commands.CreateProducuction
{
    public record CreateMovieProductionCommand : IRequest<Result<Guid>>, IMapFrom<MovieProduction>
    {
        public Guid MovieRef { get; set; }
        public string Directors { get; set; }
        public string Writers { get; set; }
        public string MovieStars { get; set; }
    }



    internal class CreateMovieProductionCommandHandler : IRequestHandler<CreateMovieProductionCommand, Result<Guid>>
    {
        private readonly ITrackCreationProgressRepository _tracker;
        private readonly IMovieBaseInfoRepository _infoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMovieProductionCommandHandler(ITrackCreationProgressRepository tracker, IMovieBaseInfoRepository infoRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _infoRepository = infoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tracker = tracker;
        }


        public async Task<Result<Guid>> Handle(CreateMovieProductionCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (await _infoRepository.CheckMovieExistence(command.MovieRef) == false)
                {
                    throw new NotFoundException("This movie does not exist.");
                }

                var producers = new MovieProduction()
                {
                    MovieRef = command.MovieRef,
                    Directors = command.Directors,
                    Writers = command.Writers,
                    MovieStars = command.MovieStars
                };

                await _unitOfWork.Repository<MovieProduction>().InsertAsync(producers);
                producers.AddDomainEvent(new MovieProductionCreatedEvent(producers));

                await _tracker.UpdateTracker(producers.MovieRef);
                await _unitOfWork.Save(cancellationToken);

                return await Result<Guid>.SuccessAsync(producers.MovieRef, "MovieProduction Created Successfully.");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ResponseConstants.MovieExistOrNull, ex);
            }   
        }
    }
}

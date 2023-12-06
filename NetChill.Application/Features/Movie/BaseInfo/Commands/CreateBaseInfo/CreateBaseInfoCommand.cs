using MediatR;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Common.Mappings;
using NetChill.Application.Interfaces.Repositories;
using NetChill.Application.Interfaces.Repositories.Movie;
using NetChill.Domain.Entities.Movie;
using NetChill.Domain.Events.Movie.BaseInfo;
using NetChill.Shared;
using System.ComponentModel.DataAnnotations;

namespace NetChill.Application.Features.Movie.BaseInfo.Commands.CreateInfo
{
    public record CreateBaseInfoCommand : IRequest<Result<Guid>>, IMapFrom<MovieBaseInfo>
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Languages { get; set; }
        public bool IsFeatured { get; set; }
        public int YearReleased { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AvailableFrom { get; set; }
    }

    internal class CreateMovieInfoCommandHandler : IRequestHandler<CreateBaseInfoCommand, Result<Guid>>
    {
        private readonly ITrackCreationProgressRepository _tracker;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CreateMovieInfoCommandHandler(ITrackCreationProgressRepository tracker,    IUnitOfWork unitOfWork, IMediator mediator)
        {
            _tracker = tracker;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(CreateBaseInfoCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var baseInfo = new MovieBaseInfo()
                {
                    Title = command.Title,
                    Genre = command.Genre,
                    Description = command.Description,
                    Languages = command.Languages,
                    IsFeatured = command.IsFeatured,
                    YearReleased = command.YearReleased,
                    AvailableFrom = command.AvailableFrom,
                };

                await _unitOfWork.Repository<MovieBaseInfo>().InsertAsync(baseInfo);
                baseInfo.AddDomainEvent(new MovieBaseCreatedEvent(baseInfo));
                
                await _tracker.InitiateTracker(baseInfo.MovieId);
                await _unitOfWork.Save(cancellationToken);

                return await Result<Guid>.SuccessAsync(baseInfo.MovieId, "MovieBaseInfo Created Successfully.");

            }
            catch (Exception ex)
            {
                throw new BadRequestException(ConstantText.Error520, ex);
            }
        }
    }
}

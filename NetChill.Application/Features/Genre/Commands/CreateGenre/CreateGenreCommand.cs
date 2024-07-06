using AutoMapper;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Common.Mappings;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Domain.Entities.Movie;
using NetChill.Domain.Events.Genre;
using NetChill.Shared;

namespace NetChill.Application.Features.Genre.Commands.CreateGenre
{
    public record CreateGenreCommand : IRequest<Result<int>>, IMapFrom<MovieGenre>
    {
        public string GenreName { get; set; }
        public string GenreDescription { get; set; }
    }

    internal class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateGenreCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var genre = new MovieGenre()
                {
                    GenreName = command.GenreName,
                    GenreDescription = command.GenreDescription
                };

                await _unitOfWork.Repository<MovieGenre>().InsertAsync(genre);
                genre.AddDomainEvent(new GenreCreatedEvent(genre));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(genre.GenreId, "Genre Created Successfully.");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ResponseConstants.Error520, ex);
            }
        }
    }
}

using AutoMapper;
using MediatR;
using NetChill.Application.Interfaces.Repositories;
using NetChill.Domain.Entities.Movie;
using NetChill.Domain.Events.Genre;
using NetChill.Shared;

namespace NetChill.Application.Features.Genre.Commands.UpdateGenre
{
    public record UpdateGenreCommand : IRequest<Result<int>>
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string GenreDescription { get; set; }
    }


    internal class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGenreCommandHandler(IUnitOfWork unitOfWorkm, IMapper mapper)
        {
            _unitOfWork = unitOfWorkm;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateGenreCommand command, CancellationToken cancellationToken)
        {
            var genre = await _unitOfWork.Repository<MovieGenre>().GetByIntIdAsync(command.GenreId);

            if (genre != null)
            {
                genre.IntId = command.GenreId;
                genre.GenreName = command.GenreName;
                genre.GenreDescription = command.GenreDescription;

                await _unitOfWork.Repository<MovieGenre>().UpdateAsyncWithIntId(genre);
                genre.AddDomainEvent(new GenreUpdatedEvent(genre));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(genre.IntId, "Genre Updated Successfully");
            }
            else
            {
                return await Result<int>.FailureAsync("Genre Not Found.");
            }
        }
    }
}

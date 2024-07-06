using AutoMapper;
using MediatR;
using NetChill.Application.Abstractions.Repositories;
using NetChill.Application.Common.Exceptions;
using NetChill.Application.Common.Mappings;
using NetChill.Application.Features.Movie.Accessories;
using NetChill.Domain.Entities.Movie;
using NetChill.Domain.Events.Genre;
using NetChill.Shared;

namespace NetChill.Application.Features.Genre.Commands.DeleteGenre
{
    public record DeleteGenreCommand : IRequest<Result<int>>, IMapFrom<MovieGenre>
    {
        public int GenreId { get; set; }

        public DeleteGenreCommand()
        {
            
        }

        public DeleteGenreCommand(int genreId)
        {
            GenreId = genreId;
        }
    }


    internal class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteGenreCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<int>> Handle(DeleteGenreCommand command, CancellationToken cancellationToken)
        {
            var genre = await _unitOfWork.Repository<MovieGenre>().GetByIdAsync(command.GenreId);

            if (genre != null)
            {
                try
                {
                    await _unitOfWork.Repository<MovieGenre>().DeleteAsync(genre);
                    genre.AddDomainEvent(new GenreDeletedEvent(genre));

                    await _unitOfWork.Save(cancellationToken);

                    return await Result<int>.SuccessAsync(genre.GenreId, "Genre Deleted Successfully.");
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ResponseConstants.Error520, ex);
                }
            }
            else
            {
                return await Result<int>.FailureAsync("Genre Not Found.");
            }
        }
    }

}

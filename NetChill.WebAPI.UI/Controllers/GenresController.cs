using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetChill.Application.Features.Genre.Commands.CreateGenre;
using NetChill.Application.Features.Genre.Commands.DeleteGenre;
using NetChill.Application.Features.Genre.Commands.UpdateGenre;
using NetChill.Application.Features.Genre.Queries.GetAllGenres;
using NetChill.Application.Features.Genre.Queries.GetGenreById;
using NetChill.Shared;

namespace NetChill.WebAPI.UI.Controllers
{
    public class GenresController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }


        //Get All Genres
        [HttpGet, Route("list")]
        public async Task<ActionResult<Result<List<GetAllGenresDto>>>> GetAllGenres()
        {
            return await _mediator.Send(new GetAllGenresQuery());
        }

        //Get Genre By Id
        [HttpGet, Route("read/{id}")]
        public async Task<ActionResult<Result<GetGenreByIdDto>>> GetGenreById(int id)
        {
            return await _mediator.Send(new GetGenreByIdQuery(id));
        }


        //Create Genre
        [HttpPost, Route("create")]
        public async Task<ActionResult<Result<int>>> AddGenre(CreateGenreCommand command)
        {
            return await _mediator.Send(command);
        }


        //Update Genre
        [HttpPut, Route("update/{id}")]
        public async Task<ActionResult<Result<int>>> UpdateGenre(int id, UpdateGenreCommand command)
        {
            if (id !=  command.GenreId)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }


        //Delete Genre
        [HttpDelete, Route("delete/{id}")]
        public async Task<ActionResult<Result<int>>> DeleteGenre(int id)
        {
            return await _mediator.Send(new DeleteGenreCommand(id));
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetChill.Application.Features.Genre.Commands.CreateGenre;
using NetChill.Application.Features.Genre.Commands.DeleteGenre;
using NetChill.Application.Features.Genre.Commands.UpdateGenre;
using NetChill.Application.Features.Genre.Queries.GetAllGenres;
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


        //Get All Genres Action Method
        [HttpGet]
        [Route("all-genres")]
        public async Task<ActionResult<Result<List<GetAllGenresDto>>>> GetAllGenres()
        {
            return await _mediator.Send(new GetAllGenresQuery());
        }


        //Create Genre Action Method
        [HttpPost]
        [Route("create-genre")]
        public async Task<ActionResult<Result<int>>> AddGenre(CreateGenreCommand command)
        {
            return await _mediator.Send(command);
        }


        //Update Genre Action Method
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<int>>> UpdateGenre(int id, UpdateGenreCommand command)
        {
            if (id !=  command.GenreId)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }


        //Delete Genre Action Method
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<int>>> DeleteGenre(int id)
        {
            return await _mediator.Send(new DeleteGenreCommand(id));
        }
    }
}

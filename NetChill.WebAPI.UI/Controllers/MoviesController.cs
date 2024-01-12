using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetChill.Application.Features.Movie.Clip.Commands.CreateClip;
using NetChill.Application.Features.Movie.BaseInfo.Commands.CreateInfo;
using NetChill.Application.Features.Movie.Production.Commands.CreateProducuction;
using NetChill.Shared;

namespace NetChill.WebAPI.UI.Controllers
{
    public class MoviesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        //Create Movie BaseInfo Action Method
        [HttpPost, Route("info")]
        public async Task<ActionResult<Result<Guid>>> AddMovieBaseInfo(CreateBaseInfoCommand command)
        {
            return await _mediator.Send(command);
        }


        //Create Movie Production Action Method
        [HttpPost, Route("production/{id}")]
        public async Task<ActionResult<Result<Guid>>> AddMovieProduction(Guid id, CreateMovieProductionCommand command)
        {
            if (id != command.MovieRef)
            {
                return BadRequest();
            }

            return await _mediator.Send(command);
        }


        //Upload Movie Files Action Method
        [HttpPost, Route("mediafiles/{id}")]
        //[RequestSizeLimit(50_000_000)]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<Result<Guid>>> UploadMovieFiles(Guid id, [FromForm] CreateMovieClipCommand command)
        {
            if (id != command.MovieRef)
            {
                return BadRequest();
            }

            return await _mediator.Send(command);
        }
    }
}

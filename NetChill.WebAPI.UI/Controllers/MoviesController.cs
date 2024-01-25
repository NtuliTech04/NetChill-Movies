using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetChill.Application.Features.Movie.Clip.Commands.CreateClip;
using NetChill.Application.Features.Movie.BaseInfo.Commands.CreateInfo;
using NetChill.Application.Features.Movie.Production.Commands.CreateProducuction;
using NetChill.Shared;
using NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllBaseInfo;
using NetChill.Application.Features.Movie.Production.Queries.GetAllProduction;
using NetChill.Application.Features.Movie.Clip.Queries.GetAllMovieClips;
using NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllUpcoming;
using NetChill.Application.Features.Movie.Clip.Queries.GetAllUpcomingMovieClips;

namespace NetChill.WebAPI.UI.Controllers
{
    public class MoviesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region List Action Methods

        //List Movie BaseInfoes
        [HttpGet, Route("info/list")]
        public async Task<ActionResult<Result<List<GetAllBaseInfoDto>>>> ListMovieBaseInfoes()
        {
            return await _mediator.Send(new GetAllBaseInfoQuery());
        }

        //List Movie Productions
        [HttpGet, Route("production/list")]
        public async Task<ActionResult<Result<List<GetAllProductionDto>>>> ListMovieProductions()
        {
            return await _mediator.Send(new GetAllProductionQuery());
        }

        //List Movie Files
        [HttpGet, Route("mediafiles/list")]
        public async Task<ActionResult<Result<List<GetAllMovieClipsDto>>>> ListMovieFiles()
        {
            return await _mediator.Send(new GetAllMovieClipsQuery());
        }

        #endregion

        #region Organised Lists Action Methods

        //List Upcoming Movies Info
        [HttpGet, Route("upcoming-info/list")]
        public async Task<ActionResult<Result<List<GetAllUpcomingDto>>>> ListUpcomingMoviesInfo()
        {
            return await _mediator.Send(new GetAllUpcomingQuery());
        }

        //List Upcoming Movies Files
        [HttpGet, Route("upcoming-media/list")]
        public async Task<ActionResult<Result<List<GetAllUpcomingMovieClipsDto>>>> ListUpcomingMoviesFiles()
        {
            return await _mediator.Send(new GetAllUpcomingMovieClipsQuery());
        }

        #endregion

        #region Create Action Methods

        //Create Movie BaseInfo
        [HttpPost, Route("info")]
        public async Task<ActionResult<Result<Guid>>> AddMovieBaseInfo(CreateBaseInfoCommand command)
        {
            return await _mediator.Send(command);
        }


        //Create Movie Production
        [HttpPost, Route("production/{id}")]
        public async Task<ActionResult<Result<Guid>>> AddMovieProduction(Guid id, CreateMovieProductionCommand command)
        {
            if (id != command.MovieRef)
            {
                return BadRequest();
            }

            return await _mediator.Send(command);
        }


        //Create/Upload Movie Files
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

        #endregion

        #region Read Action Methods

        #endregion

        #region Update Action Methods

        #endregion

        #region Delete Action Methods

        #endregion
    }
}

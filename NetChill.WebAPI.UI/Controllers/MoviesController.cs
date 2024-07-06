using MediatR;
using NetChill.Shared;
using Microsoft.AspNetCore.Mvc;
using NetChill.Application.Features.Movie.Clip.Commands.CreateClip;
using NetChill.Application.Features.Movie.BaseInfo.Commands.CreateInfo;
using NetChill.Application.Features.Movie.Production.Commands.CreateProducuction;
using NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllBaseInfo;
using NetChill.Application.Features.Movie.Clip.Queries.GetAllMovieClips;
using NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllUpcoming;
using NetChill.Application.Features.Movie.Clip.Queries.GetAllUpcomingMovieClips;
using NetChill.Application.Features.Movie.Clip.Queries.GetAllLatestMovieClips;
using NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllLatestMoviesInfo;
using NetChill.Application.Features.Movie.BaseInfo.Queries.GetAllFeaturedMoviesInfo;
using NetChill.Application.Features.Movie.Clip.Queries.GetAllFeaturedMovieClips;
using NetChill.Application.Features.Movie.BaseInfo.Queries.GetMovieInfoById;
using NetChill.Application.Features.Movie.Production.Queries.GetMovieProductionById;
using NetChill.Application.Features.Movie.Clip.Queries.GetMovieClipByRef;

namespace NetChill.WebAPI.UI.Controllers
{
    public class MoviesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region List All Action Methods

        //List Movie BaseInfoes
        [HttpGet, Route("info/list")]
        public async Task<ActionResult<Result<List<GetAllBaseInfoDto>>>> ListMovieBaseInfoes()
        {
            return await _mediator.Send(new GetAllBaseInfoQuery());
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


        //List Latest Movies Info
        [HttpGet, Route("latest-info/list")]
        public async Task<ActionResult<Result<List<GetAllLatestMoviesInfoDto>>>> ListLatestMoviesInfo()
        {
            return await _mediator.Send(new GetAllLatestMoviesInfoQuery());
        }


        //List Latest Movies Files
        [HttpGet, Route("latest-media/list")]
        public async Task<ActionResult<Result<List<GetAllLatestMovieClipsDto>>>> ListLatestMoviesFiles()
        {
            return await _mediator.Send(new GetAllLatestMovieClipsQuery());
        }


        //List Featured Movies Info
        [HttpGet, Route("featured-info/list")]
        public async Task<ActionResult<Result<List<GetAllFeaturedMoviesInfoDto>>>> ListFeaturedMoviesInfo()
        {
            return await _mediator.Send(new GetAllFeaturedMoviesInfoQuery());
        }


        //List Featured Movies Files
        [HttpGet, Route("featured-media/list")]
        public async Task<ActionResult<Result<List<GetAllFeaturedMovieClipsDto>>>> ListFeaturedMoviesFiles()
        {
            return await _mediator.Send(new GetAllFeaturedMovieClipsQuery());
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

        //Get Movie Info By Id
        [HttpGet, Route("read-info/{id}")]
        public async Task<ActionResult<Result<GetMovieInfoByIdDto>>> GetMovieInfoById(Guid id)
        {
            return await _mediator.Send(new GetMovieInfoByIdQuery(id));
        }


        //Get Movie Production By MovieRef
        [HttpGet, Route("read-production/{id}")]
        public async Task<ActionResult<Result<GetMovieProductionByRefDto>>> GetMovieProductionByRef(Guid id)
        {
            return await _mediator.Send(new GetMovieProductionByRefQuery(id));
        }


        //Get Movie Files By MovieRef
        [HttpGet, Route("read-files/{id}")]
        public async Task<ActionResult<Result<GetMovieClipByRefDto>>> GetMovieFilesById(Guid id)
        {
            return await _mediator.Send(new GetMovieClipByRefQuery(id));
        }


        #endregion

        #region Update Action Methods

        #endregion

        #region Delete Action Methods

        #endregion
    }
}

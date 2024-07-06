using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetChill.Application.Features.Language.Commands.CreateLanguage;
using NetChill.Application.Features.Language.Commands.DeleteLanguage;
using NetChill.Application.Features.Language.Commands.UpdateLanguage;
using NetChill.Application.Features.Language.Queries.GetAllLanguages;
using NetChill.Application.Features.Language.Queries.GetLanguageById;
using NetChill.Shared;

namespace NetChill.WebAPI.UI.Controllers
{
    public class LanguagesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public LanguagesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        //Get All Languages
        [HttpGet, Route("list")]
        public async Task<ActionResult<Result<List<GetAllLanguagesDto>>>> GetAllLanguages()
        {
            return await _mediator.Send(new GetAllLanguagesQuery());
        }


        //Get Language By Id
        [HttpGet, Route("read/{id}")]
        public async Task<ActionResult<Result<GetLanguageByIdDto>>> GetLanguageById(int id)
        {
            return await _mediator.Send(new GetLanguageByIdQuery(id));
        }


        //Create Language
        [HttpPost, Route("create")]
        public async Task<ActionResult<Result<int>>> AddLanguage(CreateLanguageCommand command)
        {
            return await _mediator.Send(command);
        }



        //Update Language
        [HttpPut, Route("update/{id}")]
        public async Task<ActionResult<Result<int>>> UpdateLanguage(int id, UpdateLanguageCommand command)
        {
            if (id != command.LanguageId)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }


        //Delete Language 
        [HttpDelete, Route("delete/{id}")]
        public async Task<ActionResult<Result<int>>> DeleteLanguage(int id)
        {
            return await _mediator.Send(new DeleteLanguageCommand(id));
        }
    }
}

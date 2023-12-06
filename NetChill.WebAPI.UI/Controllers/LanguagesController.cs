using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetChill.Application.Features.Language.Commands.CreateLanguage;
using NetChill.Application.Features.Language.Commands.DeleteLanguage;
using NetChill.Application.Features.Language.Commands.UpdateLanguage;
using NetChill.Application.Features.Language.Queries.GetAllLanguages;
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


        //Get All Languages Action Method
        [HttpGet]
        [Route("all-languages")]
        public async Task<ActionResult<Result<List<GetAllLanguagesDto>>>> GetAllLanguages()
        {
            return await _mediator.Send(new GetAllLanguagesQuery());
        }


        //Create Language Action Method
        [HttpPost]
        [Route("create-language")]
        public async Task<ActionResult<Result<int>>> AddLanguage(CreateLanguageCommand command)
        {
            return await _mediator.Send(command);
        }


        //Update Language Action Method
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<int>>> UpdateLanguage(int id, UpdateLanguageCommand command)
        {
            if (id != command.LanguageId)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }


        //Delete Language Action Method
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<int>>> DeleteLanguage(int id)
        {
            return await _mediator.Send(new DeleteLanguageCommand(id));
        }
    }
}

using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Commands.DeleteLanguage;
using Application.Features.Languages.Commands.UpdateLanguage;
using Application.Features.Languages.Dtos;
using Application.Features.Languages.Models;
using Application.Features.Languages.Queries.GetByIdLanguage;
using Application.Features.Languages.Queries.GetListLanguage;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateLanguageCommand createLanguageCommand)
        {
            CreateLanguageDto result = await Mediator.Send(createLanguageCommand);
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListLanguageQuery getListLanguageQuery = new() { PageRequest = pageRequest };
            LanguageListModel result = await Mediator.Send(getListLanguageQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdLanguageQuery getByIdIdLanguageQuery)
        {
            LanguageGetByIdDto languageGetByIdDto = await Mediator.Send(getByIdIdLanguageQuery);
            return Ok(languageGetByIdDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteLanguageCommand deletelanguageCommand)
        { 
            DeleteLanguageDto result = await Mediator.Send(deletelanguageCommand);
            return Created("", result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateLanguageCommand updateLanguageCommand) { 
            UpdateLanguageDto result = await Mediator.Send(updateLanguageCommand);
            return Ok(result);
        }  
    }
}

using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetAllProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProgrammingLanguagesController:BaseController
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
        {
            CreateProgrammingLanguageResultDto createProgrammingLanguageResultDto = await Mediator.Send(createProgrammingLanguageCommand);
            return Created("", createProgrammingLanguageResultDto);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
        {
            UpdateProgrammingLanguageResultDto updateProgrammingLanguageResultDto = await Mediator.Send(updateProgrammingLanguageCommand);
            return Ok(updateProgrammingLanguageResultDto);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteProgrammingLanguageCommand deleteProgrammingLanguageCommand)
        {
            DeleteProgrammingLanguageResultDto deleteProgrammingLanguageResultDto = await Mediator.Send(deleteProgrammingLanguageCommand);
            return Ok(deleteProgrammingLanguageResultDto);
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProgrammingLanguageQuery getByIdProgrammingLanguageQuery)
        {
            GetByIdProgrammingLanguageResultDto getByIdProgrammingLanguageResultDto = await Mediator.Send(getByIdProgrammingLanguageQuery);
            return Ok(getByIdProgrammingLanguageResultDto);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProgrammingLanguageQuery getAllProgrammingLanguageQuery)
        {
            GetAllProgrammingLanguageResultModel getAllProgrammingLanguageResultModel = await Mediator.Send(getAllProgrammingLanguageQuery);
            return Ok(getAllProgrammingLanguageResultModel);
        }
    }
}

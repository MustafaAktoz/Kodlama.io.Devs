using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetAllByDynamicTechnology;
using Application.Features.Technologies.Queries.GetAllTechnology;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class TechnologiesController:BaseController
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
            CreateTechnologyResultDto createTechnologyResultDto = await Mediator.Send(createTechnologyCommand);
            return Created("", createTechnologyResultDto);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTechnologyCommand updateTechnologyCommand)
        {
            UpdateTechnologyResultDto updateTechnologyResultDto = await Mediator.Send(updateTechnologyCommand);
            return Ok(updateTechnologyResultDto);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteTechnologyCommand deleteTechnologyCommand)
        {
            DeleteTechnologyResultDto deleteTechnologyResultDto = await Mediator.Send(deleteTechnologyCommand);
            return Ok(deleteTechnologyResultDto);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTechnologyQuery getAllTechnologyQuery)
        {
            GetAllTechnologyResultModel getAllTechnologyResultModel = await Mediator.Send(getAllTechnologyQuery);
            return Ok(getAllTechnologyResultModel);
        }

        [HttpGet("getAllByDynamic")]
        public async Task<IActionResult> GetAllByDynamic([FromQuery] GetAllByDynamicTechnologyQuery getAllByDynamicTechnologyQuery)
        {
            GetAllTechnologyResultModel getAllTechnologyResultModel = await Mediator.Send(getAllByDynamicTechnologyQuery);
            return Ok(getAllTechnologyResultModel);
        }
    }
}

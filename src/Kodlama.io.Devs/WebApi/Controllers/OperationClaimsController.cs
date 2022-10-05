using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Queries.GetAllOperationClaim;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class OperationClaimsController:BaseController
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreateOperationClaimResultDto createOperationClaimResultDto = await Mediator.Send(createOperationClaimCommand);
            return Created("", createOperationClaimResultDto);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            UpdateOperationClaimResultDto updateOperationClaimResultDto = await Mediator.Send(updateOperationClaimCommand);
            return Ok(updateOperationClaimResultDto);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            DeleteOperationClaimResultDto deleteOperationClaimResultDto = await Mediator.Send(deleteOperationClaimCommand);
            return Ok(deleteOperationClaimResultDto);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllOperationClaimQuery getAllOperationClaimQuery)
        {
            GetAllOperationClaimResultModel getAllOperationClaimResultModel = await Mediator.Send(getAllOperationClaimQuery);
            return Ok(getAllOperationClaimResultModel);
        }
    }
}

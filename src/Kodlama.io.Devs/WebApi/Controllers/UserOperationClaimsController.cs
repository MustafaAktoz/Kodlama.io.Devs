using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Queries.GetAllOperationClaim;
using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Queries.GetAllByUserIdUserOperationClaim;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UserOperationClaimsController:BaseController
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
        {
            CreateUserOperationClaimResultDto createUserOperationClaimResultDto = await Mediator.Send(createUserOperationClaimCommand);
            return Created("", createUserOperationClaimResultDto);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
        {
            DeleteUserOperationClaimResultDto deleteUserOperationClaimResultDto = await Mediator.Send(deleteUserOperationClaimCommand);
            return Ok(deleteUserOperationClaimResultDto);
        }

        [HttpGet("getAllByUserId")]
        public async Task<IActionResult> GetAllByUserId([FromQuery] GetAllByUserIdUserOperationClaimQuery getAllByUserIdUserOperationClaimQuery)
        {
            GetAllByUserIdUserOperationClaimResultModel getAllByUserIdUserOperationClaimResultModel = await Mediator.Send(getAllByUserIdUserOperationClaimQuery);
            return Ok(getAllByUserIdUserOperationClaimResultModel);
        }
    }
}

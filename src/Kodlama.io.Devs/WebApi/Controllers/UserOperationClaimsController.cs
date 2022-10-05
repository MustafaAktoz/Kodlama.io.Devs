using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Queries.GetAllOperationClaim;
using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Queries.GetAllUserOperationClaim;
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

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
        {
            UpdateUserOperationClaimResultDto updateUserOperationClaimResultDto = await Mediator.Send(updateUserOperationClaimCommand);
            return Ok(updateUserOperationClaimResultDto);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
        {
            DeleteUserOperationClaimResultDto deleteUserOperationClaimResultDto = await Mediator.Send(deleteUserOperationClaimCommand);
            return Ok(deleteUserOperationClaimResultDto);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUserOperationClaimQuery getAllUserOperationClaimQuery)
        {
            GetAllUserOperationClaimResultModel getAllUserOperationClaimResultModel = await Mediator.Send(getAllUserOperationClaimQuery);
            return Ok(getAllUserOperationClaimResultModel);
        }
    }
}

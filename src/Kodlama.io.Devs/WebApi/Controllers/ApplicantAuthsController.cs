using Application.Features.ApplicantAuths.Commands.LoginApplicantAuth;
using Application.Features.ApplicantAuths.Commands.RegisterApplicantAuth;
using Application.Features.ApplicantAuths.Dto;
using Application.Features.Applicants.Dtos;
using Application.Features.Applicants.Queries.GetByEmailApplicant;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ApplicantAuthsController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterApplicantAuthCommand registerApplicantAuthCommand)
        {
            RegisterApplicantAuthResultDto registerApplicantAuthResultDto = await Mediator.Send(registerApplicantAuthCommand);
            return Created("", registerApplicantAuthResultDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginApplicantAuthCommand loginApplicantAuthCommand)
        {
            LoginApplicantAuthResultDto loginApplicantAuthResultDto = await Mediator.Send(loginApplicantAuthCommand);
            return Ok(loginApplicantAuthResultDto);
        }
    }
}

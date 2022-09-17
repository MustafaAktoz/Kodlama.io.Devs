using Application.Features.ApplicantAuths.Commands.RegisterApplicantAuth;
using Application.Features.ApplicantAuths.Dtos;
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
    }
}

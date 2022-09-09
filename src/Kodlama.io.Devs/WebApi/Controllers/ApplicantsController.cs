using Application.Features.Applicants.Commands.AddGitHubAddressApplicant;
using Application.Features.Applicants.Commands.DeleteGitHubAddressApplicant;
using Application.Features.Applicants.Commands.UpdateGitHubAddressApplicant;
using Application.Features.Applicants.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ApplicantsController:BaseController
    {
        [HttpPost("addGitHubAddess")]
        public async Task<IActionResult> AddGitHubAddress([FromBody] AddGitHubAddressApplicantCommand addGitHubAddressApplicantCommand)
        {
            AddGitHubAddressApplicantResultDto addGitHubAddressApplicantResultDto = await Mediator.Send(addGitHubAddressApplicantCommand);
            return Ok(addGitHubAddressApplicantResultDto);
        }

        [HttpPost("updateGitHubAddess")]
        public async Task<IActionResult> UpdateGitHubAddress([FromBody] UpdateGitHubAddressApplicantCommand updateGitHubAddressApplicantCommand)
        {
            UpdateGitHubAddressApplicantResultDto updateGitHubAddressApplicantResultDto = await Mediator.Send(updateGitHubAddressApplicantCommand);
            return Ok(updateGitHubAddressApplicantResultDto);
        }

        [HttpPost("deleteGitHubAddess")]
        public async Task<IActionResult> UpdateGitHubAddress([FromBody] DeleteGitHubAddressApplicantCommand deleteGitHubAddressApplicantCommand)
        {
            DeleteGitHubAddressApplicantResultDto deleteGitHubAddressApplicantResultDto = await Mediator.Send(deleteGitHubAddressApplicantCommand);
            return Ok(deleteGitHubAddressApplicantResultDto);
        }
    }
}

using Application.Features.Applicants.Commands.UpdateGitHubAddressApplicant;
using Application.Features.Applicants.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ApplicantsController:BaseController
    {
        [HttpPost("updateGitHubAddess")]
        public async Task<IActionResult> UpdateGitHubAddress([FromBody] UpdateGitHubAddressApplicantCommand updateGitHubAddressApplicantCommand)
        {
            UpdateGitHubAddressApplicantResultDto updateGitHubAddressApplicantResultDto = await Mediator.Send(updateGitHubAddressApplicantCommand);
            return Ok(updateGitHubAddressApplicantResultDto);
        }
    }
}

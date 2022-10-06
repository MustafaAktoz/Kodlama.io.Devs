using Application.Features.ApplicantAuths.Commands.RegisterApplicantAuth;
using Application.Features.ApplicantAuths.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ApplicantAuthsController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterApplicantAuthCommand registerApplicantAuthCommand)
        {
            RegisterApplicantAuthResultDto registerApplicantAuthResultDto = await Mediator.Send(registerApplicantAuthCommand);
            SetRefreshTokenToCookie(registerApplicantAuthResultDto.RefreshToken);
            return Created("", registerApplicantAuthResultDto.AccessToken);
        }

        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}

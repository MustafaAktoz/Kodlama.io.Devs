using Application.Features.UserAuths.Commands.LoginUserAuth;
using Application.Features.UserAuths.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UserAuthsController:BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserAuthCommand loginUserAuthCommand)
        {
            LoginUserAuthResultDto loginUserAuthResultDto = await Mediator.Send(loginUserAuthCommand);
            SetRefreshTokenToCookie(loginUserAuthResultDto.RefreshToken);
            return Ok(loginUserAuthResultDto.AccessToken);
        }


        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}

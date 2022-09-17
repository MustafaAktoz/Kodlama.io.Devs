using Application.Features.UserAuths.Commands.LoginUserAuth;
using Application.Features.UserAuths.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UserAuthsController:BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserAuthCommand loginUserAuthCommand)
        {
            LoginUserAuthResultDto loginUserAuthResultDto = await Mediator.Send(loginUserAuthCommand);
            return Ok(loginUserAuthResultDto);
        }
    }
}

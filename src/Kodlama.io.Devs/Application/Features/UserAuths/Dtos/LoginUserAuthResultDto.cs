using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.UserAuths.Dtos
{
    public class LoginUserAuthResultDto
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}

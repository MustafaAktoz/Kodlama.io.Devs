using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserAuthService
{
    public interface IUserAuthService
    {
        Task<AccessToken> CreateAccessToken(User user);
        Task<RefreshToken> CreateRefreshToken(User user);
        Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
        Task EmailCanNotBeDuplicatedWhenRegistered(string email);
    }
}

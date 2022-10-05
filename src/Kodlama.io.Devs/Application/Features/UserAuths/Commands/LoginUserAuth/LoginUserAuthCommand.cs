using Application.Features.UserAuths.Dtos;
using Application.Features.UserAuths.Rules;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.Security.Entities;
using Application.Services.UserAuthService;
using Core.Security.JWT;
using Core.Security.Dtos;

namespace Application.Features.UserAuths.Commands.LoginUserAuth
{
    public class LoginUserAuthCommand : IRequest<LoginUserAuthResultDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class LoginUserAuthCommandHandler : IRequestHandler<LoginUserAuthCommand, LoginUserAuthResultDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserAuthService _userAuthService;
            private readonly UserAuthBusinessRules _userAuthBusinessRules;

            public LoginUserAuthCommandHandler(IUserRepository userRepository, IUserAuthService userAuthService, UserAuthBusinessRules userAuthBusinessRules)
            {
                _userRepository = userRepository;
                _userAuthService = userAuthService;
                _userAuthBusinessRules = userAuthBusinessRules;
            }

            public async Task<LoginUserAuthResultDto> Handle(LoginUserAuthCommand request, CancellationToken cancellationToken)
            {
                await _userAuthBusinessRules.MustBeAValidEmailWhenLoggedIn(request.UserForLoginDto.Email);

                User? user = await _userRepository.GetAsync(u=>u.Email == request.UserForLoginDto.Email);
                await _userAuthBusinessRules.AValidPasswordMustBeEnteredWhenLoggedIn(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken accessToken = await _userAuthService.CreateAccessToken(user);
                RefreshToken refreshToken = await _userAuthService.CreateRefreshToken(user, request.IpAddress);
                RefreshToken addedRefreshToken = await _userAuthService.AddRefreshToken(refreshToken);
                LoginUserAuthResultDto loginUserAuthResultDto = new() { AccessToken = accessToken, RefreshToken = refreshToken};

                return loginUserAuthResultDto;
            }
        }
    }
}

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
using Core.Security.AbstractObjects;

namespace Application.Features.UserAuths.Commands.LoginUserAuth
{
    public class LoginUserAuthCommand : IUserForLogin, IRequest<LoginUserAuthResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AuthenticatorCode { get; set; }

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
                await _userAuthBusinessRules.MustBeAValidEmailWhenLoggedIn(request.Email);

                User? getByEmailUserResult = await _userRepository.GetAsync(u=>u.Email == request.Email);
                await _userAuthBusinessRules.AValidPasswordMustBeEnteredWhenLoggedIn(request.Password, getByEmailUserResult.PasswordHash, getByEmailUserResult.PasswordSalt);

                AccessToken createAccessTokenResult = await _userAuthService.CreateAccessToken(getByEmailUserResult);
                RefreshToken createRefreshTokenResult = await _userAuthService.CreateRefreshToken(getByEmailUserResult);
                RefreshToken addRefreshTokenResult = await _userAuthService.AddRefreshToken(createRefreshTokenResult);
                LoginUserAuthResultDto loginUserAuthResultDto = new() { AccessToken = createAccessTokenResult, RefreshToken = addRefreshTokenResult };

                return loginUserAuthResultDto;
            }
        }
    }
}

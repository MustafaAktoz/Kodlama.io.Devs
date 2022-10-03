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

namespace Application.Features.UserAuths.Commands.LoginUserAuth
{
    public class LoginUserAuthCommand : IRequest<LoginUserAuthResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginUserAuthCommandHandler : IRequestHandler<LoginUserAuthCommand, LoginUserAuthResultDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserAuthService _userAuthService;
            private readonly IMapper _mapper;
            private readonly UserAuthBusinessRules _userAuthBusinessRules;

            public LoginUserAuthCommandHandler(IUserRepository userRepository, IUserAuthService userAuthService, IMapper mapper, UserAuthBusinessRules userAuthBusinessRules)
            {
                _userRepository = userRepository;
                _userAuthService = userAuthService;
                _mapper = mapper;
                _userAuthBusinessRules = userAuthBusinessRules;
            }

            public async Task<LoginUserAuthResultDto> Handle(LoginUserAuthCommand request, CancellationToken cancellationToken)
            {
                await _userAuthBusinessRules.MustBeAValidEmailWhenLoggedIn(request.Email);

                User? user = await _userRepository.GetAsync(u=>u.Email == request.Email);
                await _userAuthBusinessRules.AValidPasswordMustBeEnteredWhenLoggedIn(request.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken accessToken = await _userAuthService.CreateAccessToken(user);
                LoginUserAuthResultDto loginUserAuthResultDto = _mapper.Map<LoginUserAuthResultDto>(accessToken);

                return loginUserAuthResultDto;
            }
        }
    }
}

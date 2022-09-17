using Application.Features.UserAuths.Commands.CreateAccessTokenUserAuth;
using Application.Features.UserAuths.Dtos;
using Application.Features.UserAuths.Rules;
using Application.Features.Users.Queries.GetByEmailUser;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;

namespace Application.Features.UserAuths.Commands.LoginUserAuth
{
    public class LoginUserAuthCommand : IRequest<LoginUserAuthResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginUserAuthCommandHandler : IRequestHandler<LoginUserAuthCommand, LoginUserAuthResultDto>
        {
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly UserAuthBusinessRules _userAuthBusinessRules;

            public LoginUserAuthCommandHandler(IMediator mediator, IMapper mapper, UserAuthBusinessRules userAuthBusinessRules)
            {
                _mediator = mediator;
                _mapper = mapper;
                _userAuthBusinessRules = userAuthBusinessRules;
            }

            public async Task<LoginUserAuthResultDto> Handle(LoginUserAuthCommand request, CancellationToken cancellationToken)
            {
                await _userAuthBusinessRules.MustBeAValidEmailWhenLoggedIn(request.Email);

                GetByEmailUserQuery getByEmailUserQuery = _mapper.Map<GetByEmailUserQuery>(request);
                GetByEmailUserResultDto getByEmailUserResultDto = await _mediator.Send(getByEmailUserQuery);
                await _userAuthBusinessRules.AValidPasswordMustBeEnteredWhenLoggedIn(request.Password, getByEmailUserResultDto.PasswordHash, getByEmailUserResultDto.PasswordSalt);

                CreateAccessTokenUserAuthCommand createAccessTokenUserAuthCommand = _mapper.Map<CreateAccessTokenUserAuthCommand>(getByEmailUserResultDto);
                CreateAccessTokenUserAuthResultDto createAccessTokenUserAuthResultDto = await _mediator.Send(createAccessTokenUserAuthCommand);
                LoginUserAuthResultDto loginUserAuthResultDto = _mapper.Map<LoginUserAuthResultDto>(createAccessTokenUserAuthResultDto);

                return loginUserAuthResultDto;
            }
        }
    }
}

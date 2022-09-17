using Application.Features.UserAuths.Commands.CreateAccessTokenUserAuth;
using Application.Features.ApplicantAuths.Dtos;
using Application.Features.Applicants.Commands.CreateApplicant;
using Application.Features.Applicants.Dtos;
using Application.Features.UserAuths.Dtos;
using AutoMapper;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.UserAuths.Rules;

namespace Application.Features.ApplicantAuths.Commands.RegisterApplicantAuth
{
    public class RegisterApplicantAuthCommand : IRequest<RegisterApplicantAuthResultDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class RegisterApplicantAuthCommandHandler : IRequestHandler<RegisterApplicantAuthCommand, RegisterApplicantAuthResultDto>
        {
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly UserAuthBusinessRules _userAuthBusinessRules;

            public RegisterApplicantAuthCommandHandler(IMediator mediator, IMapper mapper, UserAuthBusinessRules userAuthBusinessRules)
            {
                _mediator = mediator;
                _mapper = mapper;
                _userAuthBusinessRules = userAuthBusinessRules;
            }

            public async Task<RegisterApplicantAuthResultDto> Handle(RegisterApplicantAuthCommand request, CancellationToken cancellationToken)
            {
                await _userAuthBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.Email);

                CreateApplicantCommand createApplicantCommand = _mapper.Map<CreateApplicantCommand>(request);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                createApplicantCommand.PasswordHash = passwordHash;
                createApplicantCommand.PasswordSalt = passwordSalt;
                createApplicantCommand.Status = true;
                CreateApplicantResultDto createApplicantResultDto = await _mediator.Send(createApplicantCommand);

                CreateAccessTokenUserAuthCommand createAccessTokenUserAuthCommand = _mapper.Map<CreateAccessTokenUserAuthCommand>(createApplicantResultDto);
                CreateAccessTokenUserAuthResultDto createAccessTokenUserAuthResultDto = await _mediator.Send(createAccessTokenUserAuthCommand);
                RegisterApplicantAuthResultDto registerApplicantAuthResultDto = _mapper.Map<RegisterApplicantAuthResultDto>(createAccessTokenUserAuthResultDto);

                return registerApplicantAuthResultDto;
            }
        }
    }
}

using Application.Features.ApplicantAuths.Commands.CreateAccessTokenApplicantAuth;
using Application.Features.ApplicantAuths.Dto;
using Application.Features.ApplicantAuths.Rules;
using Application.Features.Applicants.Commands.CreateApplicant;
using Application.Features.Applicants.Dtos;
using AutoMapper;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            private readonly ApplicantAuthBusinessRules _applicantAuthBusinessRules;

            public RegisterApplicantAuthCommandHandler(IMediator mediator, IMapper mapper, ApplicantAuthBusinessRules applicantAuthBusinessRules)
            {
                _mediator = mediator;
                _mapper = mapper;
                _applicantAuthBusinessRules = applicantAuthBusinessRules;
            }

            public async Task<RegisterApplicantAuthResultDto> Handle(RegisterApplicantAuthCommand request, CancellationToken cancellationToken)
            {
                await _applicantAuthBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.Email);

                CreateApplicantCommand createApplicantCommand = _mapper.Map<CreateApplicantCommand>(request);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                createApplicantCommand.PasswordHash = passwordHash;
                createApplicantCommand.PasswordSalt = passwordSalt;
                createApplicantCommand.Status = true;
                CreateApplicantResultDto createApplicantResultDto = await _mediator.Send(createApplicantCommand);

                CreateAccessTokenApplicantAuthCommand createAccessTokenApplicantAuthCommand = _mapper.Map<CreateAccessTokenApplicantAuthCommand>(createApplicantResultDto);
                CreateAccessTokenApplicantAuthResultDto createAccessTokenApplicantAuthResultDto = await _mediator.Send(createAccessTokenApplicantAuthCommand);
                RegisterApplicantAuthResultDto registerApplicantAuthResultDto = _mapper.Map<RegisterApplicantAuthResultDto>(createAccessTokenApplicantAuthResultDto);

                return registerApplicantAuthResultDto;
            }
        }
    }
}

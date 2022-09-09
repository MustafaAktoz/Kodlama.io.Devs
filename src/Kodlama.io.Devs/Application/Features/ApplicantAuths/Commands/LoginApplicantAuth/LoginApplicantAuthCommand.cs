using Application.Features.ApplicantAuths.Commands.CreateAccessTokenApplicantAuth;
using Application.Features.ApplicantAuths.Dto;
using Application.Features.ApplicantAuths.Rules;
using Application.Features.Applicants.Dtos;
using Application.Features.Applicants.Queries.GetByEmailApplicant;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ApplicantAuths.Commands.LoginApplicantAuth
{
    public class LoginApplicantAuthCommand:IRequest<LoginApplicantAuthResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginApplicantAuthCommandHandler : IRequestHandler<LoginApplicantAuthCommand, LoginApplicantAuthResultDto>
        {
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly ApplicantAuthBusinessRules _applicantAuthBusinessRules;

            public LoginApplicantAuthCommandHandler(IMediator mediator, IMapper mapper, ApplicantAuthBusinessRules applicantAuthBusinessRules)
            {
                _mediator = mediator;
                _mapper = mapper;
                _applicantAuthBusinessRules = applicantAuthBusinessRules;
            }

            public async Task<LoginApplicantAuthResultDto> Handle(LoginApplicantAuthCommand request, CancellationToken cancellationToken)
            {
                await _applicantAuthBusinessRules.MustBeAValidEmailWhenLoggedIn(request.Email);

                GetByEmailApplicantQuery getByEmailApplicantQuery = _mapper.Map<GetByEmailApplicantQuery>(request);
                GetByEmailApplicantResultDto getByEmailApplicantResultDto = await _mediator.Send(getByEmailApplicantQuery);
                await _applicantAuthBusinessRules.AValidPasswordMustBeEnteredWhenLoggedIn(request.Password, getByEmailApplicantResultDto.PasswordHash, getByEmailApplicantResultDto.PasswordSalt);

                CreateAccessTokenApplicantAuthCommand createAccessTokenApplicantAuthCommand = _mapper.Map<CreateAccessTokenApplicantAuthCommand>(getByEmailApplicantResultDto);
                CreateAccessTokenApplicantAuthResultDto createAccessTokenApplicantAuthResultDto = await _mediator.Send(createAccessTokenApplicantAuthCommand);
                LoginApplicantAuthResultDto loginApplicantAuthResultDto = _mapper.Map<LoginApplicantAuthResultDto>(createAccessTokenApplicantAuthResultDto);

                return loginApplicantAuthResultDto;
            }
        }
    }
}

using Application.Features.ApplicantAuths.Dtos;
using AutoMapper;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.UserAuths.Rules;
using Application.Services.UserAuthService;
using Domain.Entities;
using Core.Security.JWT;
using Application.Services.Repositories;

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
            private readonly IUserAuthService _userAuthService;
            private readonly IApplicantRepository _applicantRepository;
            private readonly IMapper _mapper;
            private readonly UserAuthBusinessRules _userAuthBusinessRules;

            public RegisterApplicantAuthCommandHandler(IUserAuthService userAuthService, IApplicantRepository applicantRepository, IMapper mapper, UserAuthBusinessRules userAuthBusinessRules)
            {
                _userAuthService = userAuthService;
                _applicantRepository = applicantRepository;
                _mapper = mapper;
                _userAuthBusinessRules = userAuthBusinessRules;
            }

            public async Task<RegisterApplicantAuthResultDto> Handle(RegisterApplicantAuthCommand request, CancellationToken cancellationToken)
            {
                await _userAuthBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                Applicant applicant = _mapper.Map<Applicant>(request);
                applicant.PasswordHash = passwordHash;
                applicant.PasswordSalt = passwordSalt;
                applicant.Status = true;

                Applicant addedApplicant = await _applicantRepository.AddAsync(applicant);
                AccessToken accessToken = await _userAuthService.CreateAccessToken(addedApplicant);
                RegisterApplicantAuthResultDto registerApplicantAuthResultDto = _mapper.Map<RegisterApplicantAuthResultDto>(accessToken);

                return registerApplicantAuthResultDto;
            }
        }
    }
}

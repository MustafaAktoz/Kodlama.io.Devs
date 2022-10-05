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
using Core.Security.Entities;
using Core.Security.Dtos;

namespace Application.Features.ApplicantAuths.Commands.RegisterApplicantAuth
{
    public class RegisterApplicantAuthCommand : IRequest<RegisterApplicantAuthResultDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }

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
                await _userAuthBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                Applicant applicant = _mapper.Map<Applicant>(request.UserForRegisterDto);
                applicant.PasswordHash = passwordHash;
                applicant.PasswordSalt = passwordSalt;
                applicant.Status = true;

                Applicant addedApplicant = await _applicantRepository.AddAsync(applicant);
                AccessToken createdAccessToken = await _userAuthService.CreateAccessToken(addedApplicant);
                RefreshToken createdRefreshToken = await _userAuthService.CreateRefreshToken(addedApplicant, request.IpAddress);
                RefreshToken addedRefreshToken = await _userAuthService.AddRefreshToken(createdRefreshToken);
                RegisterApplicantAuthResultDto registerApplicantAuthResultDto = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };

                return registerApplicantAuthResultDto;
            }
        }
    }
}

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
using Core.Security.AbstractObjects;

namespace Application.Features.ApplicantAuths.Commands.RegisterApplicantAuth
{
    public class RegisterApplicantAuthCommand : IUserForRegister, IRequest<RegisterApplicantAuthResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class RegisterApplicantAuthCommandHandler : IRequestHandler<RegisterApplicantAuthCommand, RegisterApplicantAuthResultDto>
        {
            private readonly IUserAuthService _userAuthService;
            private readonly IApplicantRepository _applicantRepository;
            private readonly IMapper _mapper;

            public RegisterApplicantAuthCommandHandler(IUserAuthService userAuthService, IApplicantRepository applicantRepository, IMapper mapper)
            {
                _userAuthService = userAuthService;
                _applicantRepository = applicantRepository;
                _mapper = mapper;
            }

            public async Task<RegisterApplicantAuthResultDto> Handle(RegisterApplicantAuthCommand request, CancellationToken cancellationToken)
            {
                await _userAuthService.EmailCanNotBeDuplicatedWhenRegistered(request.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                Applicant applicant = _mapper.Map<Applicant>(request);
                applicant.PasswordHash = passwordHash;
                applicant.PasswordSalt = passwordSalt;
                applicant.Status = true;

                Applicant addApplicantResult = await _applicantRepository.AddAsync(applicant);
                AccessToken createAccessTokenResult = await _userAuthService.CreateAccessToken(addApplicantResult);
                RefreshToken createRefreshTokenResult = await _userAuthService.CreateRefreshToken(addApplicantResult);
                RefreshToken addRefreshTokenResult = await _userAuthService.AddRefreshToken(createRefreshTokenResult);
                RegisterApplicantAuthResultDto registerApplicantAuthResultDto = new() { AccessToken = createAccessTokenResult, RefreshToken = addRefreshTokenResult };

                return registerApplicantAuthResultDto;
            }
        }
    }
}

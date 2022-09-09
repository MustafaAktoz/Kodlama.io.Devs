using Application.Features.ApplicantAuths.Dto;
using Application.Features.Applicants.Models;
using Application.Features.Applicants.Queries.GetClaimsApplicant;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ApplicantAuths.Commands.CreateAccessTokenApplicantAuth
{
    public class CreateAccessTokenApplicantAuthCommand:IRequest<CreateAccessTokenApplicantAuthResultDto>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public class CreateAccessTokenApplicantAuthCommandHandler : IRequestHandler<CreateAccessTokenApplicantAuthCommand, CreateAccessTokenApplicantAuthResultDto>
        {
            private readonly IMediator _mediator;
            private readonly ITokenHelper _tokenHelper;
            private readonly IMapper _mapper;

            public CreateAccessTokenApplicantAuthCommandHandler(IMediator mediator, ITokenHelper tokenHelper, IMapper mapper)
            {
                _mediator = mediator;
                _tokenHelper = tokenHelper;
                _mapper = mapper;
            }

            public async Task<CreateAccessTokenApplicantAuthResultDto> Handle(CreateAccessTokenApplicantAuthCommand request, CancellationToken cancellationToken)
            {
                Applicant applicant = _mapper.Map<Applicant>(request);
                GetClaimsApplicantQuery getClaimsApplicantQuery = _mapper.Map<GetClaimsApplicantQuery>(request);
                GetClaimsApplicantResultModel getClaimsApplicantResultModel = await _mediator.Send(getClaimsApplicantQuery);
                List<OperationClaim> operationClaims = _mapper.Map<List<OperationClaim>>(getClaimsApplicantResultModel.GetClaimsApplicantResultDtos);
                AccessToken accessToken = _tokenHelper.CreateToken(applicant, operationClaims);
                CreateAccessTokenApplicantAuthResultDto createAccessTokenApplicantAuthResultDto = _mapper.Map<CreateAccessTokenApplicantAuthResultDto>(accessToken);

                return createAccessTokenApplicantAuthResultDto;
            }
        }
    }
}

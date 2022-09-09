using Application.Features.Applicants.Dtos;
using Application.Features.Applicants.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Applicants.Queries.GetClaimsApplicant
{
    public class GetClaimsApplicantQuery:IRequest<GetClaimsApplicantResultModel>
    {
        public int Id { get; set; }

        public class GetClaimsApplicantQueryHandler : IRequestHandler<GetClaimsApplicantQuery, GetClaimsApplicantResultModel>
        {
            private readonly IApplicantRepository _applicantRepository;
            private readonly IMapper _mapper;

            public GetClaimsApplicantQueryHandler(IApplicantRepository applicantRepository, IMapper mapper)
            {
                _applicantRepository = applicantRepository;
                _mapper = mapper;
            }

            public async Task<GetClaimsApplicantResultModel> Handle(GetClaimsApplicantQuery request, CancellationToken cancellationToken)
            {
                Applicant applicant = _mapper.Map<Applicant>(request);
                IPaginate<OperationClaim> operationClaims = await _applicantRepository.GetClaims(applicant);
                GetClaimsApplicantResultModel getClaimsApplicantResultModel = _mapper.Map<GetClaimsApplicantResultModel>(operationClaims);

                return getClaimsApplicantResultModel;
            }
        }
    }
}

using Application.Features.Applicants.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Applicants.Queries.GetByEmailApplicant
{
    public class GetByEmailApplicantQuery:IRequest<GetByEmailApplicantResultDto>
    {
        public string Email { get; set; }

        public class GetByEmailApplicantQueryHandler : IRequestHandler<GetByEmailApplicantQuery, GetByEmailApplicantResultDto>
        {
            private readonly IApplicantRepository _applicantRepository;
            private readonly IMapper _mapper;

            public GetByEmailApplicantQueryHandler(IApplicantRepository applicantRepository, IMapper mapper)
            {
                _applicantRepository = applicantRepository;
                _mapper = mapper;
            }

            public async Task<GetByEmailApplicantResultDto> Handle(GetByEmailApplicantQuery request, CancellationToken cancellationToken)
            {
                Applicant? applicant = await _applicantRepository.GetAsync(
                    a => a.Email == request.Email,
                    include: i => i.Include(a => a.UserOperationClaims)
                    );
                GetByEmailApplicantResultDto getByEmailApplicantResultDto = _mapper.Map<GetByEmailApplicantResultDto>(applicant);

                return getByEmailApplicantResultDto;
            }
        }
    }
}

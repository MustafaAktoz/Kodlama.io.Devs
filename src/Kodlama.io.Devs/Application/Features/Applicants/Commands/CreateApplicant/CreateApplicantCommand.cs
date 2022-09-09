using Application.Features.Applicants.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Applicants.Commands.CreateApplicant
{
    public class CreateApplicantCommand:IRequest<CreateApplicantResultDto>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
        public string GitHubAddress { get; set; }

        public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, CreateApplicantResultDto>
        {
            private readonly IApplicantRepository _applicantRepository;
            private readonly IMapper _mapper;

            public CreateApplicantCommandHandler(IApplicantRepository applicantRepository, IMapper mapper)
            {
                _applicantRepository = applicantRepository;
                _mapper = mapper;
            }

            public async Task<CreateApplicantResultDto> Handle(CreateApplicantCommand request, CancellationToken cancellationToken)
            {
                Applicant applicant = _mapper.Map<Applicant>(request);
                Applicant addedApplicant = await _applicantRepository.AddAsync(applicant);
                CreateApplicantResultDto createApplicantResultDto = _mapper.Map<CreateApplicantResultDto>(addedApplicant);

                return createApplicantResultDto;
            }
        }
    }
}

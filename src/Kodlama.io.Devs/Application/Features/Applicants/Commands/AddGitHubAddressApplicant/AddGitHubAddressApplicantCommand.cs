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

namespace Application.Features.Applicants.Commands.AddGitHubAddressApplicant
{
    public class AddGitHubAddressApplicantCommand:IRequest<AddGitHubAddressApplicantResultDto>
    {
        public int Id { get; set; }
        public string GitHubAddress { get; set; }

        public class AddGitHubAddressApplicantCommandHandler : IRequestHandler<AddGitHubAddressApplicantCommand, AddGitHubAddressApplicantResultDto>
        {
            private readonly IApplicantRepository _applicantRepository;
            private readonly IMapper _mapper;

            public AddGitHubAddressApplicantCommandHandler(IApplicantRepository applicantRepository, IMapper mapper)
            {
                _applicantRepository = applicantRepository;
                _mapper = mapper;
            }

            public async Task<AddGitHubAddressApplicantResultDto> Handle(AddGitHubAddressApplicantCommand request, CancellationToken cancellationToken)
            {
                Applicant? applicant = await _applicantRepository.GetAsync(a=>a.Id ==request.Id);
                applicant.GitHubAddress = request.GitHubAddress;
                Applicant updatedApplicant = await _applicantRepository.UpdateAsync(applicant);
                AddGitHubAddressApplicantResultDto addGitHubAddressApplicantResultDto = _mapper.Map<AddGitHubAddressApplicantResultDto>(updatedApplicant);

                return addGitHubAddressApplicantResultDto;
            }
        }
    }
}

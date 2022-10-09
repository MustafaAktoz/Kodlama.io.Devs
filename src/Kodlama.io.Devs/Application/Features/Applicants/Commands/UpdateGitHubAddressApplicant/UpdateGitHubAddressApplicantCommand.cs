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

namespace Application.Features.Applicants.Commands.UpdateGitHubAddressApplicant
{
    public class UpdateGitHubAddressApplicantCommand:IRequest<UpdateGitHubAddressApplicantResultDto>
    {
        public int Id { get; set; }
        public string? GitHubAddress { get; set; }

        public class UpdateGitHubAddressApplicantCommandHandler : IRequestHandler<UpdateGitHubAddressApplicantCommand, UpdateGitHubAddressApplicantResultDto>
        {
            private readonly IApplicantRepository _applicantRepository;
            private readonly IMapper _mapper;

            public UpdateGitHubAddressApplicantCommandHandler(IApplicantRepository applicantRepository, IMapper mapper)
            {
                _applicantRepository = applicantRepository;
                _mapper = mapper;
            }

            public async Task<UpdateGitHubAddressApplicantResultDto> Handle(UpdateGitHubAddressApplicantCommand request, CancellationToken cancellationToken)
            {
                Applicant? getByIdApplicantResult = await _applicantRepository.GetAsync(a => a.Id == request.Id);
                getByIdApplicantResult.GitHubAddress = request.GitHubAddress;
                Applicant updateApplicantResult = await _applicantRepository.UpdateAsync(getByIdApplicantResult);
                UpdateGitHubAddressApplicantResultDto updateGitHubAddressApplicantResultDto = _mapper.Map<UpdateGitHubAddressApplicantResultDto>(updateApplicantResult);

                return updateGitHubAddressApplicantResultDto;
            }
        }
    }
}

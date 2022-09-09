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

namespace Application.Features.Applicants.Commands.DeleteGitHubAddressApplicant
{
    public class DeleteGitHubAddressApplicantCommand:IRequest<DeleteGitHubAddressApplicantResultDto>
    {
        public int Id { get; set; }

        public class DeleteGitHubAddressApplicantCommandHandler : IRequestHandler<DeleteGitHubAddressApplicantCommand, DeleteGitHubAddressApplicantResultDto>
        {
            private readonly IApplicantRepository _applicantRepository;
            private readonly IMapper _mapper;

            public DeleteGitHubAddressApplicantCommandHandler(IApplicantRepository applicantRepository, IMapper mapper)
            {
                _applicantRepository = applicantRepository;
                _mapper = mapper;
            }
            public async Task<DeleteGitHubAddressApplicantResultDto> Handle(DeleteGitHubAddressApplicantCommand request, CancellationToken cancellationToken)
            {
                Applicant? applicant = await _applicantRepository.GetAsync(a => a.Id == request.Id);
                applicant.GitHubAddress = null;
                Applicant deletedApplicant = await _applicantRepository.UpdateAsync(applicant);
                DeleteGitHubAddressApplicantResultDto deleteGitHubAddressApplicantResultDto = _mapper.Map<DeleteGitHubAddressApplicantResultDto>(deletedApplicant);

                return deleteGitHubAddressApplicantResultDto;
            }
        }
    }
}

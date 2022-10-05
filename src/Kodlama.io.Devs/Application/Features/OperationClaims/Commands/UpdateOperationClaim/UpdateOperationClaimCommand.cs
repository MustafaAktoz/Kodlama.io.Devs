using Application.Enums;
using Application.Features.OperationClaims.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommand:IRequest<UpdateOperationClaimResultDto>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdateOperationClaimResultDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;

            public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
            }

            public async Task<UpdateOperationClaimResultDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim);
                UpdateOperationClaimResultDto updateOperationClaimResultDto = _mapper.Map<UpdateOperationClaimResultDto>(updatedOperationClaim);

                return updateOperationClaimResultDto;
            }
        }
    }
}

using Application.Enums;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
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

namespace Application.Features.OperationClaims.Commands.CreateOperationClaim
{
    public class CreateOperationClaimCommand:IRequest<CreateOperationClaimResultDto>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public string Name { get; set; }

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreateOperationClaimResultDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<CreateOperationClaimResultDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.NameCanNotBeDuplicatedWhenCreated(request.Name);

                OperationClaim operationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim addOperationClaimResult = await _operationClaimRepository.AddAsync(operationClaim);
                CreateOperationClaimResultDto createOperationClaimResultDto = _mapper.Map<CreateOperationClaimResultDto>(addOperationClaimResult);

                return createOperationClaimResultDto;
            }
        }
    }
}

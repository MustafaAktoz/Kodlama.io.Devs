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

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim
{
    public class DeleteOperationClaimCommand : IRequest<DeleteOperationClaimResultDto>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int Id { get; set; }

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeleteOperationClaimResultDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;

            public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
            }

            public async Task<DeleteOperationClaimResultDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim? getByIdOperationClaimResult = await _operationClaimRepository.GetAsync(oc => oc.Id == request.Id);
                OperationClaim deleteOperationClaimResult = await _operationClaimRepository.DeleteAsync(getByIdOperationClaimResult);
                DeleteOperationClaimResultDto deleteOperationClaimResultDto = _mapper.Map<DeleteOperationClaimResultDto>(deleteOperationClaimResult);

                return deleteOperationClaimResultDto;
            }
        }
    }
}

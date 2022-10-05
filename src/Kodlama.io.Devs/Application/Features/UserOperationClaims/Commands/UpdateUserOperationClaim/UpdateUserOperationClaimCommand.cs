using Application.Enums;
using Application.Features.UserOperationClaims.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaim
{
    public class UpdateUserOperationClaimCommand : IRequest<UpdateUserOperationClaimResultDto>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdateUserOperationClaimResultDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<UpdateUserOperationClaimResultDto> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim updatedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(userOperationClaim);
                UserOperationClaim? getUserOperationClaimResult = await _userOperationClaimRepository.GetAsync(uoc=>uoc.Id == updatedUserOperationClaim.Id, include:i=>i.Include(uoc=>uoc.User).Include(uoc=>uoc.OperationClaim));
                UpdateUserOperationClaimResultDto updateUserOperationClaimResultDto = _mapper.Map<UpdateUserOperationClaimResultDto>(getUserOperationClaimResult);

                return updateUserOperationClaimResultDto;
            }
        }
    }
}

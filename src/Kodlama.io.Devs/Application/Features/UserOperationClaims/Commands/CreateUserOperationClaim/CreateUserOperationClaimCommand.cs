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

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand : IRequest<CreateUserOperationClaimResultDto>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreateUserOperationClaimResultDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<CreateUserOperationClaimResultDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim addedUserOperationClaim = await _userOperationClaimRepository.AddAsync(userOperationClaim);
                UserOperationClaim? getUserOperationClaimResult = await _userOperationClaimRepository.GetAsync(uoc => uoc.Id == addedUserOperationClaim.Id, include: i => i.Include(uoc => uoc.User).Include(uoc => uoc.OperationClaim));
                CreateUserOperationClaimResultDto createUserOperationClaimResultDto = _mapper.Map<CreateUserOperationClaimResultDto>(getUserOperationClaimResult);

                return createUserOperationClaimResultDto;
            }
        }
    }
}

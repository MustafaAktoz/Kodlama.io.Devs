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

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimCommand : IRequest<DeleteUserOperationClaimResultDto>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int Id { get; set; }

        public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeleteUserOperationClaimResultDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<DeleteUserOperationClaimResultDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(uoc => uoc.Id == request.Id, include: i => i.Include(uoc => uoc.User).Include(uoc => uoc.OperationClaim));
                UserOperationClaim deletedUserOperationClaim = await _userOperationClaimRepository.DeleteAsync(userOperationClaim);
                DeleteUserOperationClaimResultDto deleteUserOperationClaimResultDto = _mapper.Map<DeleteUserOperationClaimResultDto>(deletedUserOperationClaim);

                return deleteUserOperationClaimResultDto;
            }
        }
    }
}

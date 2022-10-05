using Application.Enums;
using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries.GetAllUserOperationClaim
{
    public class GetAllUserOperationClaimQuery : IPageRequest, IRequest<GetAllUserOperationClaimResultModel>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetAllUserOperationClaimQueryHandler : IRequestHandler<GetAllUserOperationClaimQuery, GetAllUserOperationClaimResultModel>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public GetAllUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<GetAllUserOperationClaimResultModel> Handle(GetAllUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(include:i=>i.Include(uoc=>uoc.User).Include(uoc=>uoc.OperationClaim));
                GetAllUserOperationClaimResultModel getAllUserOperationClaimResultModel = _mapper.Map<GetAllUserOperationClaimResultModel>(userOperationClaims);

                return getAllUserOperationClaimResultModel;
            }
        }
    }
}

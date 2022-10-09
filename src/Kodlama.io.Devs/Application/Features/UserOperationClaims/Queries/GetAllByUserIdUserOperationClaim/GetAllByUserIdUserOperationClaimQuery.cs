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

namespace Application.Features.UserOperationClaims.Queries.GetAllByUserIdUserOperationClaim
{
    public class GetAllByUserIdUserOperationClaimQuery : IPageRequest, IRequest<GetAllByUserIdUserOperationClaimResultModel>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetAllByUserIdUserOperationClaimQueryHandler : IRequestHandler<GetAllByUserIdUserOperationClaimQuery, GetAllByUserIdUserOperationClaimResultModel>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public GetAllByUserIdUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<GetAllByUserIdUserOperationClaimResultModel> Handle(GetAllByUserIdUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> getListByUserIdUserOperationClaimResult = await _userOperationClaimRepository.GetListAsync(uoc => uoc.UserId == request.UserId, include:i=>i.Include(uoc=>uoc.User).Include(uoc=>uoc.OperationClaim));
                GetAllByUserIdUserOperationClaimResultModel getAllByUserIdUserOperationClaimResultModel = _mapper.Map<GetAllByUserIdUserOperationClaimResultModel>(getListByUserIdUserOperationClaimResult);

                return getAllByUserIdUserOperationClaimResultModel;
            }
        }
    }
}

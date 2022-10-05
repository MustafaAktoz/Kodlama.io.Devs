using Application.Features.OperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetAllOperationClaim
{
    public class GetAllOperationClaimQuery : IPageRequest, IRequest<GetAllOperationClaimResultModel>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetAllOperationClaimQueryHandler : IRequestHandler<GetAllOperationClaimQuery, GetAllOperationClaimResultModel>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;

            public GetAllOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
            }

            public async Task<GetAllOperationClaimResultModel> Handle(GetAllOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync();
                GetAllOperationClaimResultModel getAllOperationClaimResultModel = _mapper.Map<GetAllOperationClaimResultModel>(operationClaims);

                return getAllOperationClaimResultModel;
            }
        }
    }
}

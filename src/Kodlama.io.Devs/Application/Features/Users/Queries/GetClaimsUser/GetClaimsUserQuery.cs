using Application.Features.Users.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetClaimsUser
{
    public class GetClaimsUserQuery : IRequest<GetClaimsUserResultModel>
    {
        public int Id { get; set; }

        public class GetClaimsUserQueryHandler : IRequestHandler<GetClaimsUserQuery, GetClaimsUserResultModel>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetClaimsUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<GetClaimsUserResultModel> Handle(GetClaimsUserQuery request, CancellationToken cancellationToken)
            {
                User user = _mapper.Map<User>(request);
                IPaginate<OperationClaim> operationClaims = await _userRepository.GetClaimsAsync(user);
                GetClaimsUserResultModel getClaimsUserResultModel = _mapper.Map<GetClaimsUserResultModel>(operationClaims);

                return getClaimsUserResultModel;
            }
        }
    }
}

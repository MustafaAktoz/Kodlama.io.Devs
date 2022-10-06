using Application.Features.UserOperationClaims.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task ThisOperationClaimCannotBeDuplicatedForThisUserWhenCreated(int userId, int operationCliamId)
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(uoc => uoc.UserId == userId && uoc.OperationClaimId == operationCliamId);
            if (userOperationClaim != null) throw new BusinessException(UserOperationClaimExceptionMessages.UserAlreadyHasThisOperationClaim);
        }
    }
}

using Application.Features.OperationClaims.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task NameCanNotBeDuplicatedWhenCreated(string name)
        {
            OperationClaim? getByNameOperationClaimResult = await _operationClaimRepository.GetAsync(oc => oc.Name == name);
            if (getByNameOperationClaimResult != null) throw new BusinessException(OperationClaimExceptionMessages.NameAlreadyExists);
        }

        public async Task NameCanNotBeDuplicatedWhenUpdated(int id, string name)
        {
            OperationClaim? getByNameOperationClaimResult = await _operationClaimRepository.GetAsync(oc => oc.Id != id && oc.Name == name);
            if (getByNameOperationClaimResult != null) throw new BusinessException(OperationClaimExceptionMessages.NameAlreadyExists);
        }
    }
}

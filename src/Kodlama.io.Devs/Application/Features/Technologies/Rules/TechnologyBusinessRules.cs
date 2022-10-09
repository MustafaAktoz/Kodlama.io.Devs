using Application.Features.Technologies.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public  class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public async Task NameCanNotBeDuplicatedWhenCreated(string name)
        {
            Technology? getByNameTechnologyResult = await _technologyRepository.GetAsync(t => t.Name == name);
            if (getByNameTechnologyResult != null) throw new BusinessException(TechnologyExceptionMessages.NameAlreadyExists);
        }

        public async Task NameCanNotBeDuplicatedWhenUpdated(int id, string name)
        {
            Technology? getByNameTechnologyResult = await _technologyRepository.GetAsync(t => t.Id != id && t.Name == name);
            if (getByNameTechnologyResult != null) throw new BusinessException(TechnologyExceptionMessages.NameAlreadyExists);
        }
    }
}

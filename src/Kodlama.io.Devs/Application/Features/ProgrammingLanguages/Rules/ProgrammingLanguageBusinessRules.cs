using Application.Features.ProgrammingLanguages.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task NameCanNotBeDuplicatedWhenCreated(string name)
        {
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(pl => pl.Name == name);
            if (programmingLanguage != null) throw new BusinessException(ProgrammingLanguageExceptionMessages.NameAlreadyExists);
        }

        public async Task NameCanNotBeDuplicatedWhenUpdated(int id, string name)
        {
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(pl => pl.Id != id && pl.Name == name);
            if (programmingLanguage != null) throw new BusinessException(ProgrammingLanguageExceptionMessages.NameAlreadyExists);
        }
    }
}

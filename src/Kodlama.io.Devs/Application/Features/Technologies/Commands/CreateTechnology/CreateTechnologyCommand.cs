using Application.Enums;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommand:IRequest<CreateTechnologyResultDto>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreateTechnologyResultDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            public readonly TechnologyBusinessRules _technologyBusinessRules;

            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<CreateTechnologyResultDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.NameCanNotBeDuplicatedWhenCreated(request.Name);

                Technology technology = _mapper.Map<Technology>(request);
                Technology addTechnologyResult = await _technologyRepository.AddAsync(technology);
                Technology? getByIdTechnologyResult = await _technologyRepository.GetAsync(t=>t.Id == addTechnologyResult.Id, include:i=>i.Include(t=>t.ProgrammingLanguage));
                CreateTechnologyResultDto createTechnologyResultDto = _mapper.Map<CreateTechnologyResultDto>(getByIdTechnologyResult);

                return createTechnologyResultDto;
            }
        }
    }
}

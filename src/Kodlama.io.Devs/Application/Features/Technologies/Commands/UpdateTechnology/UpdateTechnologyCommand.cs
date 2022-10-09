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

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand:IRequest<UpdateTechnologyResultDto>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdateTechnologyResultDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            public readonly TechnologyBusinessRules _technologyBusinessRules;

            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }
            public async Task<UpdateTechnologyResultDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.NameCanNotBeDuplicatedWhenUpdated(request.Id,request.Name);

                Technology technology = _mapper.Map<Technology>(request);
                Technology updateTechnologyResult = await _technologyRepository.UpdateAsync(technology);
                Technology? getByIdTechnologyResult = await _technologyRepository.GetAsync(t=>t.Id == updateTechnologyResult.Id, include:i=>i.Include(t=>t.ProgrammingLanguage));
                UpdateTechnologyResultDto updateTechnologyResultDto = _mapper.Map<UpdateTechnologyResultDto>(getByIdTechnologyResult);

                return updateTechnologyResultDto;
            }
        }
    }
}

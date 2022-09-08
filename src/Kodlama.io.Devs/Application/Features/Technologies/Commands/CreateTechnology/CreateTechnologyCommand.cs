﻿using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
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
    public class CreateTechnologyCommand:IRequest<CreateTechnologyResultDto>
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreateTechnologyResultDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<CreateTechnologyResultDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology technology = _mapper.Map<Technology>(request);
                Technology addedTechnology = await _technologyRepository.AddAsync(technology);
                Technology? getTechnologyResult = await _technologyRepository.GetAsync(t=>t.Id == addedTechnology.Id, include:i=>i.Include(t=>t.ProgrammingLanguage));
                CreateTechnologyResultDto createTechnologyResultDto = _mapper.Map<CreateTechnologyResultDto>(getTechnologyResult);

                return createTechnologyResultDto;
            }
        }
    }
}
using Application.Enums;
using Application.Features.Technologies.Dtos;
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

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommand:IRequest<DeleteTechnologyResultDto>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int Id { get; set; }

        public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeleteTechnologyResultDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<DeleteTechnologyResultDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology? getByIdTechnologyResult = await _technologyRepository.GetAsync(t=>t.Id == request.Id, include:i=>i.Include(t=>t.ProgrammingLanguage));
                Technology deleteTechnologyResult = await _technologyRepository.DeleteAsync(getByIdTechnologyResult);
                DeleteTechnologyResultDto deleteTechnologyResultDto = _mapper.Map<DeleteTechnologyResultDto>(deleteTechnologyResult);

                return deleteTechnologyResultDto;
            }
        }
    }
}

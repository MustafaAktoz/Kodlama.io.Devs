using Application.Enums;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageCommand:IRequest<DeleteProgrammingLanguageResultDto>, ISecuredRequest
    {
        public string[] Roles => new[] { ClaimRoles.admin.ToString() };

        public int Id { get; set; }

        public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeleteProgrammingLanguageResultDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;

            public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<DeleteProgrammingLanguageResultDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage? getByIdProgrammingLanguageResult = await _programmingLanguageRepository.GetAsync(pl => pl.Id == request.Id);
                ProgrammingLanguage deleteProgrammingLanguageResult = await _programmingLanguageRepository.DeleteAsync(getByIdProgrammingLanguageResult);
                DeleteProgrammingLanguageResultDto deleteProgrammingLanguageResultDto = _mapper.Map<DeleteProgrammingLanguageResultDto>(deleteProgrammingLanguageResult);

                return deleteProgrammingLanguageResultDto;
            }
        }
    }
}

using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Queries.GetAllProgrammingLanguage
{
    public class GetAllProgrammingLanguageQuery :IPageRequest, IRequest<GetAllProgrammingLanguageResultModel>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetAllProgrammingLanguageQueryHandler : IRequestHandler<GetAllProgrammingLanguageQuery, GetAllProgrammingLanguageResultModel>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;

            public GetAllProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<GetAllProgrammingLanguageResultModel> Handle(GetAllProgrammingLanguageQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingLanguage> getListProgrammingLanguageResult = await _programmingLanguageRepository.GetListAsync(index: request.Page, size: request.PageSize);
                GetAllProgrammingLanguageResultModel getAllProgrammingLanguageResultModel = _mapper.Map<GetAllProgrammingLanguageResultModel>(getListProgrammingLanguageResult);

                return getAllProgrammingLanguageResultModel;
            }
        }
    }
}

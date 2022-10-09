using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetAllByDynamicTechnology
{
    public class GetAllByDynamicTechnologyQuery : IPageRequest, IDynamic, IRequest<GetAllTechnologyResultModel>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<Sort>? Sort { get; set; }
        public Filter? Filter { get; set; }

        public class GetAllByDynamicTechnologyQueryHandler : IRequestHandler<GetAllByDynamicTechnologyQuery, GetAllTechnologyResultModel>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public GetAllByDynamicTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<GetAllTechnologyResultModel> Handle(GetAllByDynamicTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> getListByDynamicTechnologyResult = await _technologyRepository.GetListByDynamicAsync(
                    dynamic: _mapper.Map<Dynamic>(request),
                    include: i => i.Include(t => t.ProgrammingLanguage),
                    index: request.Page,
                    size: request.PageSize
                    );
                GetAllTechnologyResultModel getAllTechnologyResultModel = _mapper.Map<GetAllTechnologyResultModel>(getListByDynamicTechnologyResult);

                return getAllTechnologyResultModel;
            }
        }
    }
}

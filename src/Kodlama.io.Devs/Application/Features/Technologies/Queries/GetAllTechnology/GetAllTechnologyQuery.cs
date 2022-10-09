using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetAllTechnology
{
    public class GetAllTechnologyQuery : IPageRequest, IRequest<GetAllTechnologyResultModel>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetAllTechnologyQueryHandler : IRequestHandler<GetAllTechnologyQuery, GetAllTechnologyResultModel>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public GetAllTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<GetAllTechnologyResultModel> Handle(GetAllTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> getListTechnologyResult = await _technologyRepository.GetListAsync(
                    include: i => i.Include(t => t.ProgrammingLanguage),
                    index: request.Page,
                    size: request.PageSize
                    );
                GetAllTechnologyResultModel getAllTechnologyResultModel = _mapper.Map<GetAllTechnologyResultModel>(getListTechnologyResult);

                return getAllTechnologyResultModel;
            }
        }
    }
}

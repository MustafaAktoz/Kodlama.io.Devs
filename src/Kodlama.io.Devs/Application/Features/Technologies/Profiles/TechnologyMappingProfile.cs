using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetAllByDynamicTechnology;
using AutoMapper;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles
{
    public class TechnologyMappingProfile:Profile
    {
        public TechnologyMappingProfile()
        {
            CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, CreateTechnologyResultDto>().ForMember(right => right.ProgrammingLanguageName, opt => opt.MapFrom(left=>left.ProgrammingLanguage.Name)).ReverseMap();

            CreateMap<Technology, UpdateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, UpdateTechnologyResultDto>().ForMember(right => right.ProgrammingLanguageName, opt => opt.MapFrom(left => left.ProgrammingLanguage.Name)).ReverseMap();

            CreateMap<Technology, DeleteTechnologyCommand>().ReverseMap();
            CreateMap<Technology, DeleteTechnologyResultDto>().ForMember(right => right.ProgrammingLanguageName, opt => opt.MapFrom(left => left.ProgrammingLanguage.Name)).ReverseMap();

            CreateMap<IPaginate<Technology>, GetAllTechnologyResultModel>().ForMember(right=>right.GetAllTechnologyResultDtos, opt =>opt.MapFrom(left=>left.Items)).ReverseMap();
            CreateMap<Technology, GetAllTechnologyResultDto>().ForMember(right => right.ProgrammingLanguageName, opt => opt.MapFrom(left => left.ProgrammingLanguage.Name)).ReverseMap();

            CreateMap<GetAllByDynamicTechnologyQuery, Dynamic>().ReverseMap();

        }
    }
}

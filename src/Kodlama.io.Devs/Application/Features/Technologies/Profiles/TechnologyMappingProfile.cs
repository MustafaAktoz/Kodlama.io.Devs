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
            CreateMap<CreateTechnologyCommand, Technology>().ReverseMap();
            CreateMap<CreateTechnologyResultDto, Technology>().ReverseMap().ForMember(left => left.ProgrammingLanguageName, opt => opt.MapFrom(right=>right.ProgrammingLanguage.Name));

            CreateMap<UpdateTechnologyCommand, Technology>().ReverseMap();
            CreateMap<UpdateTechnologyResultDto, Technology>().ReverseMap().ForMember(left => left.ProgrammingLanguageName, opt => opt.MapFrom(right => right.ProgrammingLanguage.Name));

            CreateMap<DeleteTechnologyCommand, Technology>().ReverseMap();
            CreateMap<DeleteTechnologyResultDto, Technology>().ReverseMap().ForMember(left => left.ProgrammingLanguageName, opt => opt.MapFrom(right => right.ProgrammingLanguage.Name));

            CreateMap<GetAllTechnologyResultModel, IPaginate<Technology>>().ForMember(right=>right.Items, opt =>opt.MapFrom(left=>left.GetAllTechnologyResultDtos)).ReverseMap();
            CreateMap<GetAllTechnologyResultDto, Technology>().ReverseMap().ForMember(left => left.ProgrammingLanguageName, opt => opt.MapFrom(right => right.ProgrammingLanguage.Name));

            CreateMap<GetAllByDynamicTechnologyQuery, Dynamic>().ReverseMap();
        }
    }
}

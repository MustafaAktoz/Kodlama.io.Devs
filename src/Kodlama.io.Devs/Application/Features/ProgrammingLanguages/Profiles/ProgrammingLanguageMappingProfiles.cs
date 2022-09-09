using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Profiles
{
    public class ProgrammingLanguageMappingProfiles : Profile
    {
        public ProgrammingLanguageMappingProfiles()
        {
            CreateMap<CreateProgrammingLanguageCommand, ProgrammingLanguage>().ReverseMap();
            CreateMap<CreateProgrammingLanguageResultDto, ProgrammingLanguage>().ReverseMap();

            CreateMap<UpdateProgrammingLanguageCommand, ProgrammingLanguage>().ReverseMap();
            CreateMap<UpdateProgrammingLanguageResultDto, ProgrammingLanguage>().ReverseMap();

            CreateMap<DeleteProgrammingLanguageCommand, ProgrammingLanguage>().ReverseMap();
            CreateMap<DeleteProgrammingLanguageResultDto, ProgrammingLanguage>().ReverseMap();

            CreateMap<GetAllProgrammingLanguageResultDto, ProgrammingLanguage>().ReverseMap();
            CreateMap<GetAllProgrammingLanguageResultModel, IPaginate<ProgrammingLanguage>>().ForMember(right => right.Items, opt => opt.MapFrom(left => left.GetAllProgrammingLanguageResultDtos)).ReverseMap();

            CreateMap<GetByIdProgrammingLanguageResultDto, ProgrammingLanguage>().ReverseMap();
        }
    }
}

using Application.Features.ApplicantAuths.Commands.CreateAccessTokenApplicantAuth;
using Application.Features.ApplicantAuths.Commands.LoginApplicantAuth;
using Application.Features.ApplicantAuths.Commands.RegisterApplicantAuth;
using Application.Features.ApplicantAuths.Dto;
using Application.Features.Applicants.Commands.CreateApplicant;
using Application.Features.Applicants.Dtos;
using Application.Features.Applicants.Queries.GetByEmailApplicant;
using Application.Features.Applicants.Queries.GetClaimsApplicant;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ApplicantAuths.Profiles
{
    public class ApplicantAuthMappingProfile:Profile
    {
        public ApplicantAuthMappingProfile()
        {
            CreateMap<Applicant, CreateAccessTokenApplicantAuthCommand>().ReverseMap();
            CreateMap<AccessToken, CreateAccessTokenApplicantAuthResultDto>().ReverseMap();
            CreateMap<UserOperationClaim, OperationClaim>()
                .ForMember(right => right.Id, opt => opt.MapFrom(left => left.OperationClaim.Id))
                .ForMember(right => right.Name, opt => opt.MapFrom(left => left.OperationClaim.Name))
                .ReverseMap();
            CreateMap<CreateApplicantCommand, RegisterApplicantAuthCommand>().ReverseMap();
            CreateMap<CreateApplicantResultDto, CreateAccessTokenApplicantAuthCommand>().ReverseMap();
            CreateMap<CreateAccessTokenApplicantAuthResultDto, RegisterApplicantAuthResultDto>().ReverseMap();
            CreateMap<GetByEmailApplicantQuery, LoginApplicantAuthCommand>().ReverseMap();
            CreateMap<GetByEmailApplicantResultDto, CreateAccessTokenApplicantAuthCommand>().ReverseMap();
            CreateMap<LoginApplicantAuthResultDto, CreateAccessTokenApplicantAuthCommand>().ReverseMap();
            CreateMap<CreateAccessTokenApplicantAuthResultDto, LoginApplicantAuthResultDto>().ReverseMap();
            CreateMap<CreateAccessTokenApplicantAuthCommand, GetClaimsApplicantQuery>().ReverseMap();
        }
    }
}

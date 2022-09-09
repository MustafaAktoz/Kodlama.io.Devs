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
            CreateMap<RegisterApplicantAuthCommand, CreateApplicantCommand>().ReverseMap();
            CreateMap<RegisterApplicantAuthResultDto, CreateAccessTokenApplicantAuthResultDto>().ReverseMap();

            CreateMap<LoginApplicantAuthCommand, GetByEmailApplicantQuery>().ReverseMap();
            CreateMap<LoginApplicantAuthResultDto, CreateAccessTokenApplicantAuthResultDto>().ReverseMap();

            CreateMap<CreateAccessTokenApplicantAuthCommand, Applicant>().ReverseMap();
            CreateMap<CreateAccessTokenApplicantAuthCommand, GetClaimsApplicantQuery>().ReverseMap();
            CreateMap<CreateAccessTokenApplicantAuthCommand, GetByEmailApplicantResultDto>().ReverseMap();
            CreateMap<CreateAccessTokenApplicantAuthCommand, CreateApplicantResultDto>().ReverseMap();
            CreateMap<CreateAccessTokenApplicantAuthResultDto, AccessToken>().ReverseMap();
        }
    }
}

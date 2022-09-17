using Application.Features.UserAuths.Commands.CreateAccessTokenUserAuth;
using Application.Features.UserAuths.Commands.LoginUserAuth;
using Application.Features.ApplicantAuths.Commands.RegisterApplicantAuth;
using Application.Features.ApplicantAuths.Dtos;
using Application.Features.Applicants.Commands.CreateApplicant;
using Application.Features.Applicants.Dtos;
using Application.Features.Users.Queries.GetByEmailUser;
using Application.Features.Users.Queries.GetClaimsUser;
using Application.Features.UserAuths.Dtos;
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
            CreateMap<RegisterApplicantAuthResultDto, CreateAccessTokenUserAuthResultDto>().ReverseMap();

            CreateMap<CreateApplicantResultDto, CreateAccessTokenUserAuthCommand>().ReverseMap();
        }
    }
}

using Application.Features.ApplicantAuths.Commands.RegisterApplicantAuth;
using Application.Features.ApplicantAuths.Dtos;
using AutoMapper;
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
            CreateMap<RegisterApplicantAuthCommand, Applicant>().ReverseMap();
            CreateMap<RegisterApplicantAuthResultDto, AccessToken>().ReverseMap();
        }
    }
}

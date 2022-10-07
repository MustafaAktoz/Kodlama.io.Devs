using Application.Features.Applicants.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Applicants.Profiles
{
    public class ApplicantMappingProfile:Profile
    {
        public ApplicantMappingProfile()
        {
            CreateMap<UpdateGitHubAddressApplicantResultDto, Applicant>().ReverseMap();
        }
    }
}

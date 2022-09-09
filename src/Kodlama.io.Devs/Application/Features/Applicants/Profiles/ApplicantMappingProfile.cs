using Application.Features.ApplicantAuths.Commands.CreateAccessTokenApplicantAuth;
using Application.Features.Applicants.Commands.CreateApplicant;
using Application.Features.Applicants.Dtos;
using Application.Features.Applicants.Models;
using Application.Features.Applicants.Queries.GetClaimsApplicant;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
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
            CreateMap<CreateApplicantCommand, Applicant>().ReverseMap();
            CreateMap<CreateApplicantResultDto, Applicant>().ReverseMap();

            CreateMap<AddGitHubAddressApplicantResultDto, Applicant>().ReverseMap();

            CreateMap<UpdateGitHubAddressApplicantResultDto, Applicant>().ReverseMap();

            CreateMap<DeleteGitHubAddressApplicantResultDto, Applicant>().ReverseMap();

            CreateMap<GetByEmailApplicantResultDto, Applicant>().ReverseMap();

            CreateMap<GetClaimsApplicantQuery, Applicant>().ReverseMap();
            CreateMap<GetClaimsApplicantResultModel, IPaginate<OperationClaim>>().ForMember(right=>right.Items, opt =>opt.MapFrom(left=>left.GetClaimsApplicantResultDtos)).ReverseMap();
            CreateMap<GetClaimsApplicantResultDto, OperationClaim>().ReverseMap();

            
        }
    }
}

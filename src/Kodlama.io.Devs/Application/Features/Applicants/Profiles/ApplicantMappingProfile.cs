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
            CreateMap<Applicant, CreateApplicantCommand>().ReverseMap();
            CreateMap<Applicant, CreateApplicantResultDto>().ReverseMap();

            CreateMap<Applicant, GetByEmailApplicantResultDto>().ReverseMap();

            CreateMap<Applicant, GetClaimsApplicantQuery>().ReverseMap();

            CreateMap<IPaginate<OperationClaim>, GetClaimsApplicantResultModel>().ForMember(right=>right.GetClaimsApplicantResultDtos, opt =>opt.MapFrom(left=>left.Items)).ReverseMap();
            CreateMap<OperationClaim, GetClaimsApplicantResultDto>().ReverseMap();

            CreateMap<Applicant, AddGitHubAddressApplicantResultDto>();
            CreateMap<Applicant, UpdateGitHubAddressApplicantResultDto>();
            CreateMap<Applicant, DeleteGitHubAddressApplicantResultDto>();
        }
    }
}

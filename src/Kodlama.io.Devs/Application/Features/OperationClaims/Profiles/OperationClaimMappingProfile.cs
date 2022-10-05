using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Profiles
{
    public class OperationClaimMappingProfile:Profile
    {
        public OperationClaimMappingProfile()
        {
            CreateMap<CreateOperationClaimCommand, OperationClaim>().ReverseMap();
            CreateMap<CreateOperationClaimResultDto, OperationClaim>().ReverseMap();

            CreateMap<UpdateOperationClaimCommand, OperationClaim>().ReverseMap();
            CreateMap<UpdateOperationClaimResultDto, OperationClaim>().ReverseMap();

            CreateMap<DeleteOperationClaimResultDto, OperationClaim>().ReverseMap();

            CreateMap<GetAllOperationClaimResultModel, IPaginate<OperationClaim>>().ForMember(right => right.Items, opt => opt.MapFrom(left => left.GetAllOperationClaimResultDtos)).ReverseMap();
            CreateMap<GetAllOperationClaimResultDto, OperationClaim>().ReverseMap();
        }
    }
}

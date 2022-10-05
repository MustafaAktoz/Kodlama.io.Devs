using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class UserOperationClaimMappingProfile : Profile
    {
        public UserOperationClaimMappingProfile()
        {
            CreateMap<CreateUserOperationClaimCommand, UserOperationClaim>().ReverseMap();
            CreateMap<CreateUserOperationClaimResultDto, UserOperationClaim>().ReverseMap()
                .ForMember(left => left.ClaimName, opt => opt.MapFrom(right => right.OperationClaim.Name))
                .ForMember(left => left.UserEmail, opt => opt.MapFrom(right => right.User.Email));

            CreateMap<UpdateUserOperationClaimCommand, UserOperationClaim>().ReverseMap();
            CreateMap<UpdateUserOperationClaimResultDto, UserOperationClaim>().ReverseMap()
                .ForMember(left => left.ClaimName, opt => opt.MapFrom(right => right.OperationClaim.Name))
                .ForMember(left => left.UserEmail, opt => opt.MapFrom(right => right.User.Email));

            CreateMap<DeleteUserOperationClaimResultDto, UserOperationClaim>().ReverseMap()
                .ForMember(left => left.ClaimName, opt => opt.MapFrom(right => right.OperationClaim.Name))
                .ForMember(left => left.UserEmail, opt => opt.MapFrom(right => right.User.Email));

            CreateMap<GetAllUserOperationClaimResultModel, IPaginate<UserOperationClaim>>().ForMember(right => right.Items, opt => opt.MapFrom(left => left.GetAllUserOperationClaimResultDtos)).ReverseMap();
            CreateMap<GetAllUserOperationClaimResultDto, UserOperationClaim>().ReverseMap()
                .ForMember(left => left.ClaimName, opt => opt.MapFrom(right => right.OperationClaim.Name))
                .ForMember(left => left.UserEmail, opt => opt.MapFrom(right => right.User.Email));
        }
    }
}

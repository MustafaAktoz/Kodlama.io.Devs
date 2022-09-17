using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using Application.Features.Users.Queries.GetClaimsUser;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<GetClaimsUserQuery, User>().ReverseMap();
            CreateMap<GetClaimsUserResultModel, IPaginate<OperationClaim>>().ForMember(right => right.Items, opt => opt.MapFrom(left => left.GetClaimsUserResultDtos)).ReverseMap();
            CreateMap<GetClaimsUserResultDto, OperationClaim>().ReverseMap();

            CreateMap<GetByEmailUserResultDto, User>().ReverseMap();
        }
    }
}

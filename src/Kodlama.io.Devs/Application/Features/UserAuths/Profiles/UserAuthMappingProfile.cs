using Application.Features.UserAuths.Commands.CreateAccessTokenUserAuth;
using Application.Features.UserAuths.Commands.LoginUserAuth;
using Application.Features.UserAuths.Dtos;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries.GetByEmailUser;
using Application.Features.Users.Queries.GetClaimsUser;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserAuths.Profiles
{
    public class UserAuthMappingProfile:Profile
    {
        public UserAuthMappingProfile()
        {
            CreateMap<LoginUserAuthCommand, GetByEmailUserQuery>().ReverseMap();
            CreateMap<LoginUserAuthResultDto, CreateAccessTokenUserAuthResultDto>().ReverseMap();

            CreateMap<CreateAccessTokenUserAuthCommand, User>().ReverseMap();
            CreateMap<CreateAccessTokenUserAuthCommand, GetClaimsUserQuery>().ReverseMap();
            CreateMap<CreateAccessTokenUserAuthCommand, GetByEmailUserResultDto>().ReverseMap();
            CreateMap<CreateAccessTokenUserAuthResultDto, AccessToken>().ReverseMap();
        }
    }
}

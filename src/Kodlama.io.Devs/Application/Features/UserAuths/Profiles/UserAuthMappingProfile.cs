using Application.Features.UserAuths.Commands.LoginUserAuth;
using Application.Features.UserAuths.Dtos;
using AutoMapper;
using Core.Security.JWT;
using Domain.Entities;

namespace Application.Features.UserAuths.Profiles
{
    public class UserAuthMappingProfile:Profile
    {
        public UserAuthMappingProfile()
        {
            CreateMap<LoginUserAuthCommand, Applicant>().ReverseMap();
            CreateMap<LoginUserAuthResultDto, AccessToken>().ReverseMap();
        }
    }
}

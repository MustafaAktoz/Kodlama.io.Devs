using Application.Features.UserAuths.Dtos;
using Application.Features.Users.Queries.GetClaimsUser;
using Application.Features.Users.Models;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserAuths.Commands.CreateAccessTokenUserAuth
{
    public class CreateAccessTokenUserAuthCommand : IRequest<CreateAccessTokenUserAuthResultDto>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public class CreateAccessTokenUserAuthCommandHandler : IRequestHandler<CreateAccessTokenUserAuthCommand, CreateAccessTokenUserAuthResultDto>
        {
            private readonly IMediator _mediator;
            private readonly ITokenHelper _tokenHelper;
            private readonly IMapper _mapper;

            public CreateAccessTokenUserAuthCommandHandler(IMediator mediator, ITokenHelper tokenHelper, IMapper mapper)
            {
                _mediator = mediator;
                _tokenHelper = tokenHelper;
                _mapper = mapper;
            }

            public async Task<CreateAccessTokenUserAuthResultDto> Handle(CreateAccessTokenUserAuthCommand request, CancellationToken cancellationToken)
            {
                GetClaimsUserQuery getClaimsUserQuery = _mapper.Map<GetClaimsUserQuery>(request);
                GetClaimsUserResultModel getClaimsUserResultModel = await _mediator.Send(getClaimsUserQuery);

                User user = _mapper.Map<User>(request);
                List<OperationClaim> operationClaims = _mapper.Map<List<OperationClaim>>(getClaimsUserResultModel.GetClaimsUserResultDtos);
                AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
                CreateAccessTokenUserAuthResultDto createAccessTokenUserAuthResultDto = _mapper.Map<CreateAccessTokenUserAuthResultDto>(accessToken);

                return createAccessTokenUserAuthResultDto;
            }
        }
    }
}

using Application.Features.Users.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetByEmailUser
{
    public class GetByEmailUserQuery : IRequest<GetByEmailUserResultDto>
    {
        public string Email { get; set; }

        public class GetByEmailUserQueryHandler : IRequestHandler<GetByEmailUserQuery, GetByEmailUserResultDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetByEmailUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<GetByEmailUserResultDto> Handle(GetByEmailUserQuery request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(
                    a => a.Email == request.Email,
                    include: i => i.Include(a => a.UserOperationClaims)
                    );
                GetByEmailUserResultDto getByEmailUserResultDto = _mapper.Map<GetByEmailUserResultDto>(user);

                return getByEmailUserResultDto;
            }
        }
    }
}

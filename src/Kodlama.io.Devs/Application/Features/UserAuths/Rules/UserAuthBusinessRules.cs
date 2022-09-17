using Application.Features.UserAuths.Constants;
using Application.Features.Users.Queries.GetByEmailUser;
using Application.Features.Users.Dtos;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserAuths.Rules
{
    public class UserAuthBusinessRules
    {
        IMediator _mediator;
        public UserAuthBusinessRules(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task MustBeAValidEmailWhenLoggedIn(string email)
        {
            GetByEmailUserQuery getByEmailUserQuery = new() { Email = email };
            GetByEmailUserResultDto getByEmailUserResultDto = await _mediator.Send(getByEmailUserQuery);
            if (getByEmailUserResultDto == null) throw new BusinessException(UserAuthMessages.EmailNotFound);
        }

        public async Task AValidPasswordMustBeEnteredWhenLoggedIn(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
                throw new BusinessException(UserAuthMessages.PasswordIsIncorrect);
        }

        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            GetByEmailUserQuery getByEmailUserQuery = new() { Email = email };
            GetByEmailUserResultDto getByEmailUserResultDto = await _mediator.Send(getByEmailUserQuery);
            if (getByEmailUserResultDto != null) throw new BusinessException(UserAuthMessages.EmailIsAlreadyRegistered);
        }
    }
}

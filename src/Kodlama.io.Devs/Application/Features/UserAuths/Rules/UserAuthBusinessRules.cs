using Application.Features.UserAuths.Constants;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.Security.Entities;

namespace Application.Features.UserAuths.Rules
{
    public class UserAuthBusinessRules
    {
        IUserRepository _userRepository;

        public UserAuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task MustBeAValidEmailWhenLoggedIn(string email)
        {
            User? getByEmailUserResult = await _userRepository.GetAsync(u => u.Email == email);
            if (getByEmailUserResult == null) throw new BusinessException(UserAuthMessages.EmailNotFound);
        }

        public async Task AValidPasswordMustBeEnteredWhenLoggedIn(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
                throw new BusinessException(UserAuthMessages.PasswordIsIncorrect);
        }

        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            User? getByEmailUserResult = await _userRepository.GetAsync(u => u.Email == email);
            if (getByEmailUserResult != null) throw new BusinessException(UserAuthMessages.EmailIsAlreadyRegistered);
        }
    }
}

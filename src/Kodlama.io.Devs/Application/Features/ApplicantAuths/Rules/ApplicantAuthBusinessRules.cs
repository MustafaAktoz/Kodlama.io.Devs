using Application.Features.ApplicantAuths.Constants;
using Application.Features.Applicants.Dtos;
using Application.Features.Applicants.Queries.GetByEmailApplicant;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ApplicantAuths.Rules
{
    public class ApplicantAuthBusinessRules
    {
        IMediator _mediator;
        public ApplicantAuthBusinessRules(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task MustBeAValidEmailWhenLoggedIn(string email)
        {
            GetByEmailApplicantQuery getByEmailApplicantQuery = new() { Email = email };
            GetByEmailApplicantResultDto getByEmailApplicantResultDto = await _mediator.Send(getByEmailApplicantQuery);
            if (getByEmailApplicantResultDto == null) throw new BusinessException(ApplicantAuthMessages.EmailNotFound);
        }

        public async Task AValidPasswordMustBeEnteredWhenLoggedIn(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
                throw new BusinessException(ApplicantAuthMessages.PasswordIsIncorrect);
        }

        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            GetByEmailApplicantQuery getByEmailApplicantQuery = new() { Email = email };
            GetByEmailApplicantResultDto getByEmailApplicantResultDto = await _mediator.Send(getByEmailApplicantQuery);
            if (getByEmailApplicantResultDto != null) throw new BusinessException(ApplicantAuthMessages.EmailIsAlreadyRegistered);
        }
    }
}

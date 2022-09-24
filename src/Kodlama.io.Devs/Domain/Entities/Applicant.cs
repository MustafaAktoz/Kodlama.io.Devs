using Core.Security.Entities;
using Core.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Applicant : User
    {
        public string? GitHubAddress { get; set; }

        public Applicant()
        {

        }

        public Applicant(int id, string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash, bool status, AuthenticatorType authenticatorType, string gitHubAddress) :
            base(id, firstName, lastName, email, passwordSalt, passwordHash, status, authenticatorType)
        {
            GitHubAddress = gitHubAddress;
        }
    }
}

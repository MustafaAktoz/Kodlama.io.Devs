﻿using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Applicants.Dtos
{
    public class GetByEmailApplicantResultDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
        public string GitHubAddress { get; set; }
        public ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}

using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Applicant:User
    {
        public string? GitHubAddress { get; set; }

        public Applicant()
        {

        }

        public Applicant(string gitHubAddress):this()
        {
            GitHubAddress = gitHubAddress;
        }
    }
}

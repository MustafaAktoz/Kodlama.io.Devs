using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ApplicantAuths.Dto
{
    public class CreateAccessTokenApplicantAuthResultDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}

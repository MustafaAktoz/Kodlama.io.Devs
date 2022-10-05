using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Dtos
{
    public class CreateUserOperationClaimResultDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string ClaimName { get; set; }
    }
}

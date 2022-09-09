using Application.Features.Applicants.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Applicants.Models
{
    public class GetClaimsApplicantResultModel:BasePageableModel
    {
        public List<GetClaimsApplicantResultDto> GetClaimsApplicantResultDtos { get; set; }
    }
}

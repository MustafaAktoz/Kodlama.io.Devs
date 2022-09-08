using Application.Features.Technologies.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Models
{
    public class GetAllTechnologyResultModel:BasePageableModel
    {
        public IList<GetAllTechnologyResultDto> GetAllTechnologyResultDtos { get; set; }
    }
}

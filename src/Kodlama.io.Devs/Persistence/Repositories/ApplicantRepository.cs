using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Drawing;
using System.Threading;

namespace Persistence.Repositories
{
    public class ApplicantRepository : EfRepositoryBase<Applicant, BaseDbContext>, IApplicantRepository
    {
        public ApplicantRepository(BaseDbContext context) : base(context) { }
    }
}

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

        public async Task<IPaginate<OperationClaim>> GetClaims(Applicant applicant, int index = 0, int size = 10, CancellationToken cancellationToken = default)
        {
            var result = from uoc in Context.UserOperationClaims
                         join oc in Context.OperationClaims
                         on uoc.OperationClaimId equals oc.Id
                         where uoc.UserId == applicant.Id
                         select new OperationClaim(oc.Id, oc.Name);
            return await result.ToPaginateAsync(index, size, 0, cancellationToken);
        }
    }
}

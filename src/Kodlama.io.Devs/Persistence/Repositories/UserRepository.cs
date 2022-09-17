using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<User, BaseDbContext>, IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context) { }

        public async Task<IPaginate<OperationClaim>> GetClaimsAsync(User user, int index = 0, int size = 10, CancellationToken cancellationToken = default)
        {
            var result = from uoc in Context.UserOperationClaims
                         join oc in Context.OperationClaims
                         on uoc.OperationClaimId equals oc.Id
                         where uoc.UserId == user.Id
                         select new OperationClaim(oc.Id, oc.Name);
            return await result.ToPaginateAsync(index, size, 0, cancellationToken);
        }
    }
}

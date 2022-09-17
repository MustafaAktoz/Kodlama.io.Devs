using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories
{
    public interface IUserRepository:IAsyncRepository<User>
    {
        Task<IPaginate<OperationClaim>> GetClaimsAsync(User user, int index = 0, int size = 10, CancellationToken cancellationToken = default);
    }
}

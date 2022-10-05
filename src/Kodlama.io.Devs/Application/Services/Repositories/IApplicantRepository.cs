using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IApplicantRepository : IAsyncRepository<Applicant>
    {
    }
}

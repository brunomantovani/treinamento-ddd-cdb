using System.Threading;
using System.Threading.Tasks;

namespace CdbContext.DomainModels.Quotas
{
    public interface IQuotaRepository
    {
        Task AddAsync(Quota quota, CancellationToken cancellationToken = default);
        Task<Quota> GetQuotaByIdAsync(QuotaId quotaId, CancellationToken cancellationToken = default);
    }
}

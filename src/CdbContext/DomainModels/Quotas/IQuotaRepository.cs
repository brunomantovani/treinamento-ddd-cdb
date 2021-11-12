using System.Threading.Tasks;

namespace CdbContext.DomainModels.Quotas
{
    public interface IQuotaRepository
    {
        Task AddAsync(Quota quota);
    }
}

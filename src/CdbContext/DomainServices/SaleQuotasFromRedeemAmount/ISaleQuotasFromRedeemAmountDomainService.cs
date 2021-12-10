using CdbContext.DomainModels.Quotas;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CdbContext.DomainServices.SaleQuotasFromRedeemAmount
{
    public interface ISaleQuotasFromRedeemAmountDomainService
    {
        Task<IEnumerable<Quota>> HandleAsync(IEnumerable<Quota> quotas, CancellationToken cancellationToken);
    }
}

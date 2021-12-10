using CdbContext.DomainModels.Quotas;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CdbContext.DomainServices.SaleQuotasFromRedeemAmount
{
    public sealed class SaleQuotasFromRedeemAmountDomainService
        : ISaleQuotasFromRedeemAmountDomainService
    {
        public Task<IEnumerable<Quota>> HandleAsync(
            IEnumerable<Quota> quotas, 
            CancellationToken cancellationToken)
        {
            //todo: criar a regra e adicionar o teste de unidade
            throw new System.NotImplementedException();
        }
    }
}

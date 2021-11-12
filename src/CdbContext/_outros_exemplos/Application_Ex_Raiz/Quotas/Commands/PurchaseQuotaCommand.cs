using System;

namespace CdbContext.Application_Ex_Raiz.Quotas.Commands
{
    public sealed record PurchaseQuotaCommand(
        Guid InvestmentAccountId,
        Guid CheckingAccountId,
        decimal Amount)
    {
    }
}

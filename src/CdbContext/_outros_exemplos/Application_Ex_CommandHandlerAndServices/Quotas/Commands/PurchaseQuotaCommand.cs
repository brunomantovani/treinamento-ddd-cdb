using System;

namespace CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Commands
{
    public sealed record PurchaseQuotaCommand(
        Guid InvestmentAccountId,
        Guid CheckingAccountId,
        decimal Amount)
    {
    }
}

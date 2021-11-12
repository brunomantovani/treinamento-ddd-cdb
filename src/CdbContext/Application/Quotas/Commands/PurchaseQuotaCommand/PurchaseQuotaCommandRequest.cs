using System;

namespace CdbContext.Application.Quotas.Commands.PurchaseQuotaCommand
{
    public sealed record PurchaseQuotaCommandRequest(
        Guid InvestmentAccountId,
        Guid CheckingAccountId,
        decimal Amount)
    {
    }
}

using System;

namespace CdbContext.Application_Ex_CommandHandler.Quotas.Commands.PurchaseQuotaCommand
{
    public sealed record PurchaseQuotaCommandRequest(
        Guid InvestmentAccountId,
        Guid CheckingAccountId,
        decimal Amount)
    {
    }
}

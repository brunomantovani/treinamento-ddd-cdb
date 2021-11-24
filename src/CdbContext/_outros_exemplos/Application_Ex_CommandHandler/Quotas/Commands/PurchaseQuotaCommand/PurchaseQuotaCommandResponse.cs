using System;

namespace CdbContext.Application_Ex_CommandHandler.Quotas.Commands.PurchaseQuotaCommand
{
    public sealed record PurchaseQuotaCommandResponse(
        decimal QuotaPositionAmount, DateTime UpdatedAt)
    {
    }
}

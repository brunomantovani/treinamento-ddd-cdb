using System;

namespace CdbContext.Application_Ex_MediatR.Quotas.Commands.PurchaseQuotaCommand
{
    public sealed record PurchaseQuotaCommandResponse(
        decimal QuotaPositionAmount, DateTime UpdatedAt)
    {
    }
}

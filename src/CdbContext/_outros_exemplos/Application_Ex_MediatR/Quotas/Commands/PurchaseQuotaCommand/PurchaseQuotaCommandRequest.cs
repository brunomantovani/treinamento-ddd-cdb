using MediatR;
using System;

namespace CdbContext.Application_Ex_MediatR.Quotas.Commands.PurchaseQuotaCommand
{
    public sealed record PurchaseQuotaCommandRequest(
        Guid InvestmentAccountId, Guid CheckingAccountId, decimal Amount)
        : IRequest<PurchaseQuotaCommandResponse>
    {
    }
}

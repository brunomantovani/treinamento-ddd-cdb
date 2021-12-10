using System;

namespace CdbContext.Application.Quotas.Commands.RedemptionQuotaCommand
{
    public sealed class RedemptionQuotaCommandRequest
    {

        public RedemptionQuotaCommandRequest(
            Guid investmentAccountId, 
            decimal amount)
        {
            InvestmentAccountId = investmentAccountId;
            Amount = amount;
        }

        public Guid InvestmentAccountId { get; }
        public decimal Amount { get; }
    }
}
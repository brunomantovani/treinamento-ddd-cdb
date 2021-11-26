using System;

namespace CdbContext.Application.Quotas.Commands.RedemptionQuotaCommand
{
    public class RedemptionQuotaCommandRequest
    {

        public RedemptionQuotaCommandRequest(Guid investmentAccountId)
        {
            InvestmentAccountId = investmentAccountId;
        }

        public Guid InvestmentAccountId { get; }
    }
}
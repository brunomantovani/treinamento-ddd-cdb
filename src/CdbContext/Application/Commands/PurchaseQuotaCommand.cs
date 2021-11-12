using System;

namespace CdbContext.Application.Commands
{
    public sealed class PurchaseQuotaCommand
    {
        public Guid InvestmentAccountId { get; set; }
        public Guid CheckingAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}

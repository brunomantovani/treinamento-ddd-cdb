using Commons.DomainModels;
using System;

namespace CdbContext.DomainModels.InvestmentAccounts
{
    public sealed class InvestmentAccountId
        : Identity
    {
        public InvestmentAccountId(Guid value)
            : base(value)
        {
        }
    }
}

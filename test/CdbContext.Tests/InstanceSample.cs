using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.DomainModels.Quotas;
using Commons.DomainModels;
using System;

namespace CdbContext.Tests
{
    public class InstanceSample
    {
        public InstanceSample()
        {
            var quotaAmount = new QuotaAmount(10M);
            var investmentAccountId = new InvestmentAccountId(Guid.NewGuid());

            var quota = new Quota(
                quotaAmount,
                investmentAccountId);

            //quota.Amount = new QuotaAmount(100);

            var result = quota.AddIncome(
                DateTime.Now,
                0.10M);

            quota.AddIncome(
                DateTime.Now,
                0.10M);

            var quotaId = QuotaId.NewIdentity();
            //InvestmentAccountId investmentAccountId = quotaId;

            ValueObject valueObject = new QuotaAmount(10);

            var id = Guid.NewGuid();

            var investmentAccountId1 = new InvestmentAccountId(id);
            var investmentAccountId2 = new InvestmentAccountId(id);
            var investmentAccountId3 = new InvestmentAccountId(id);
            var investmentAccountId4 = new InvestmentAccountId(id);
            var investmentAccountId5 = new InvestmentAccountId(id);
            var investmentAccountId6 = new InvestmentAccountId(id);

            investmentAccountId1.Equals(investmentAccountId6);
        }
    }
}

using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.DomainModels.Quotas;
using NUnit.Framework;
using System;

namespace CdbContext.Tests.DomainModels.Quotas
{
    [TestFixture]
    public sealed class QuotaTest
    {
        [Test]        
        public void AssertQuotaPosition()
        {
            //arrange
            var currentDate = DateTime.Today;

            var quotaAmountValue = 10M;
            var quotaAmount = new QuotaAmount(
                quotaAmountValue);

            var investmentAccountIdValue = Guid.NewGuid();
            var investmentAccountId = new InvestmentAccountId(
                investmentAccountIdValue);

            var quota = Quota.FromPurchase(
                quotaAmount,
                investmentAccountId);

            //act
            var quotaPosition = quota.GetPosition();

            //assert
            Assert.AreEqual(quotaAmount.Value, quotaPosition.Amount);
            Assert.AreEqual(currentDate, quotaPosition.UpdatedAt.Date);
        }
    }
}

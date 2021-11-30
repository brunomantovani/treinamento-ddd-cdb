using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.DomainModels.Quotas;
using System;
using NUnit.Framework;
using Test = NUnit.Framework;

namespace CdbContext.Tests.DomainModels.Quotas
{
    [TestFixture]
    public sealed class AssertQuotaPosition
    {
        private Quota _quota;
        private DateTime _currentDate;
        private decimal _quotaAmountValue;

        [SetUp]
        public void Arrange()
        {
            _currentDate = DateTime.Today;

            _quotaAmountValue = 10M;
            var quotaAmount = new QuotaAmount(
                _quotaAmountValue);

            var investmentAccountIdValue = Guid.NewGuid();
            var investmentAccountId = new InvestmentAccountId(
                investmentAccountIdValue);

            _quota = Quota.FromPurchase(
                quotaAmount,
                investmentAccountId);
        }

        [Test]
        public void Act()
        {
            var quotaPosition = _quota
                .GetPosition();

            Assert(quotaPosition);
        }

        public void Assert(
            QuotaPosition quotaPosition)
        {
            Test.Assert.AreEqual(_quotaAmountValue, quotaPosition.Amount);
            Test.Assert.AreEqual(_currentDate, quotaPosition.UpdatedAt.Date);
        }
    }
}

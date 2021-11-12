using CdbContext.DomainModels.Quotas;
using NUnit.Framework;
using System;

namespace CdbContext.Tests.DomainModels.Quotas
{
    public sealed class QuotaAmountTest
    {
        [TestCase(0)]
        [TestCase(-1)]
        public void AssertQuotaAmountPositiveValue(
            decimal quotaAmountValue)
        {
            //arrange
            var expectedExceptionMessage = "o amount não pode menor ou igual a zero";

            //act
            QuotaAmount quotaAmountCreate()
            {
                return new QuotaAmount(
                    quotaAmountValue);
            }

            //assert
            var exception = Assert
                .Throws<InvalidOperationException>(() => quotaAmountCreate());

            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        [TestCase(10, ExpectedResult = 10)]
        [TestCase(20, ExpectedResult = 20)]
        public decimal AssertQuotaAmountValue(
            decimal quotaAmountValue)
        {
            //arrange
            QuotaAmount quotaAmount;

            //act
            quotaAmount = new QuotaAmount(
                quotaAmountValue);

            //assert
            return quotaAmount;
        }
    }
}

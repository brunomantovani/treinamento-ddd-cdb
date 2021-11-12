using Commons.DomainModels;
using System;
using System.Collections.Generic;

namespace CdbContext.DomainModels.Quotas
{
    /// <summary>
    /// Rendimento da cota
    /// </summary>
    public sealed class QuotaIncome
        : ValueObject
    {
        public QuotaIncome(DateTime date, decimal amount)
        {
            Date = date;
            Amount = amount;
        }

        public DateTime Date { get; }
        public decimal Amount { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Date;
            yield return Amount;
        }
    }
}

using System;
using System.Collections.Generic;

namespace CdbContext.DomainModels.Quotas
{
    public sealed class QuotaPosition
        : Commons.DomainModels.ValueObject
    {
        public QuotaPosition(
            decimal amount,
            DateTime updatedAt)
        {
            Amount = amount;
            UpdatedAt = updatedAt;
        }

        public decimal Amount { get; }
        public DateTime UpdatedAt { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return UpdatedAt;
        }
    }
}

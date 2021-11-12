using Commons.DomainModels;
using System;
using System.Collections.Generic;

namespace CdbContext.DomainModels.Quotas
{
    public sealed class QuotaAmount
        : ValueObject
    {
        public QuotaAmount(decimal value)
        {
            if (value <= 0)
            {
                throw new InvalidOperationException("o amount não pode menor ou igual a zero");
            }

            Value = value;
        }

        public decimal Value { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator decimal(QuotaAmount quotaAmount)
        {
            return quotaAmount.Value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
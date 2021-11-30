using Commons.DomainModels;
using System.Collections.Generic;

namespace CdbContext.DomainModels.Quotas
{
    public sealed class QuotaAmount
        : ValueObject
    {
        public QuotaAmount(decimal value)
        {
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

        public override bool Equals(object obj)
        {
            if(obj.GetType() == typeof(decimal))
            {
                return (decimal)obj == Value;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
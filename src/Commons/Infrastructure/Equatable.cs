using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Commons.Infrastructure
{
    public abstract class Equatable
        : IEquatable<Equatable>
    {
        protected static bool EqualOperator(Equatable left, Equatable right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }
            return left is null || left.Equals(right);
        }

        protected static bool NotEqualOperator(Equatable left, Equatable right)
        {
            return !EqualOperator(left, right);
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            Equatable other = (Equatable)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (thisValues.Current is null ^
                    otherValues.Current is null)
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public bool Equals([AllowNull] Equatable other)
        {
            return other.Equals(obj: this);
        }
    }
}

using System;

namespace Commons.DomainModels
{
    public abstract class Identity
        : IEquatable<Identity>
        , IIdentity
    {
        protected Identity()
        {
        }

        public Identity(
            Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public virtual bool Equals(Identity identity)
        {
            if (ReferenceEquals(this, identity)) return true;
            if (identity is null) return false;

            return Value.Equals(identity.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Identity);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        public static implicit operator Guid(Identity identity)
        {
            return identity.Value;
        }
    }
}

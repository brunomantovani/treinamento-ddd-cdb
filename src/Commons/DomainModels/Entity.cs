using System;

namespace Commons.DomainModels
{
    public abstract class Entity<TIdentity>
        : IEntity<TIdentity>
        , IEquatable<IEntity<TIdentity>>
        where TIdentity : IIdentity
    {
        protected Entity()
        {
        }

        protected Entity(
            TIdentity id)
        {
            Id = id;
        }

        public TIdentity Id { get; }

        public virtual bool Equals(IEntity<TIdentity> entity)
        {
            if (ReferenceEquals(this, entity)) return true;
            if (entity is null) return false;

            return Id.Equals(entity.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Identity);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }
    }
}

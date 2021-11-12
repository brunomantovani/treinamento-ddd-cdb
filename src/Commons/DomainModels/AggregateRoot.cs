using System;

namespace Commons.DomainModels
{
    public abstract class AggregateRoot<TIdentity>
        : Entity<TIdentity>
        , IAggregateRoot
        where TIdentity : IIdentity
    {
        protected AggregateRoot()
        {
        }

        protected AggregateRoot(TIdentity id)
            : base(id)
        {
            CreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get; }
    }
}

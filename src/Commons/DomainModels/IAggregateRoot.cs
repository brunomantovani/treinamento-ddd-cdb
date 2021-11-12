using System;

namespace Commons.DomainModels
{
    public interface IAggregateRoot
    {
        DateTime CreatedAt { get; }
    }
}

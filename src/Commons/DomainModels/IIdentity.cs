using System;

namespace Commons.DomainModels
{
    //todo: show how refactor to accept composite keys (composite id)
    public interface IIdentity
    {
        Guid Value { get; }
    }
}

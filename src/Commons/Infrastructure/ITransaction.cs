using System;

namespace Commons.Infrastructure
{
    public interface ITransaction
        : IDisposable
    {
        void Commit();
        void Rollback();
    }
}

using System;
using System.Threading.Tasks;

namespace Commons.Infrastructure
{
    public abstract class Disposable
        : IDisposable
        , IAsyncDisposable
    {
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            await Task.Run(() => Dispose(true));
        }

        ~Disposable()
        {
            Dispose(false);
        }
    }
}

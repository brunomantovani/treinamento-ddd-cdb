using System.Threading;
using System.Threading.Tasks;

namespace Commons.Infrastructure
{
    public interface IQueryHandlerAsync<in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}

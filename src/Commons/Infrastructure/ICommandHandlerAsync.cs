using System.Threading;
using System.Threading.Tasks;

namespace Commons.Infrastructure
{
    public interface ICommandHandlerAsync<in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}

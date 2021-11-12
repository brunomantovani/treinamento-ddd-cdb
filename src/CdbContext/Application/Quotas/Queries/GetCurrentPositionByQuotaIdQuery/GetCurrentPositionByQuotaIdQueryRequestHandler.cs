using CdbContext.DomainModels.Quotas;
using Commons.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;

namespace CdbContext.Application.Quotas.Queries.GetCurrentPositionByQuotaIdQuery
{
    public sealed class GetCurrentPositionByQuotaIdQueryRequestHandler
        : IQueryHandlerAsync<GetCurrentPositionByQuotaIdQueryRequest, GetCurrentPositionByQuotaIdQueryResponse>
    {
        private readonly IMemoryCache _memoryCache;

        public GetCurrentPositionByQuotaIdQueryRequestHandler(
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<GetCurrentPositionByQuotaIdQueryResponse> HandleAsync(
            GetCurrentPositionByQuotaIdQueryRequest request,
            CancellationToken cancellationToken)
        {
            var currentPosition = _memoryCache
                   .Get<QuotaPosition>($"quota-position-{request.QuotaId}");

            if (currentPosition == null)
            {
                return null;
            }

            var result = new GetCurrentPositionByQuotaIdQueryResponse(
                currentPosition.Amount,
                currentPosition.UpdatedAt);

            return Task.FromResult(result);
        }
    }
}

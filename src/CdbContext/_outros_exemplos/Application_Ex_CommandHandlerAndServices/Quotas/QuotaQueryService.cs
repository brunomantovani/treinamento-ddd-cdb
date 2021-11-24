using CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Exceptions;
using CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Queries;
using CdbContext.DomainModels.Quotas;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas
{
    public sealed class QuotaQueryService
        : IQuotaQueryService
    {
        private readonly IMemoryCache _memoryCache;

        public QuotaQueryService(
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<GetCurrentPositionByQuotaIdQuery> Handle(
            GetCurrentPositionByQuotaIdQueryFilter filter)
        {
            var currentPosition = _memoryCache
                    .Get<QuotaPosition>($"quota-position-{filter.QuotaId}");

            if (currentPosition == null)
            {
                throw new GetCurrentPositionByQuotaIdQueryNotFoundException();
            }

            var result = new GetCurrentPositionByQuotaIdQuery
            {
                Amount = currentPosition.Amount,
                UpdatedAt = currentPosition.UpdatedAt
            };

            return Task.FromResult(result);
        }
    }
}

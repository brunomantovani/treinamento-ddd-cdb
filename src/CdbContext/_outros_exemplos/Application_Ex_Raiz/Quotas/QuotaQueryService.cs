using CdbContext.Application.Quotas.Queries;
using CdbContext.DomainModels.Quotas;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace CdbContext.Application_Ex_Raiz.Quotas
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

        public Task<CurrentPositionData> GetCurrentPositionByQuotaIdQuery(
            Guid quotaId)
        {
            var currentPosition = _memoryCache
                    .Get<QuotaPosition>($"quota-position-{quotaId}");

            if (currentPosition == null)
            {
                return null;
            }

            var result = new CurrentPositionData
            {
                Amount = currentPosition.Amount,
                UpdatedAt = currentPosition.UpdatedAt
            };

            return Task.FromResult(result);
        }
    }
}

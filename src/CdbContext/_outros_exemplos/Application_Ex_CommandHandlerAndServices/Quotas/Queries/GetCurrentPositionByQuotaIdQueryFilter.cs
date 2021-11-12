using System;

namespace CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Queries
{
    public sealed class GetCurrentPositionByQuotaIdQueryFilter
    {
        public GetCurrentPositionByQuotaIdQueryFilter(Guid quotaId)
        {
            QuotaId = quotaId;
        }

        public Guid QuotaId { get; private set; }
    }
}

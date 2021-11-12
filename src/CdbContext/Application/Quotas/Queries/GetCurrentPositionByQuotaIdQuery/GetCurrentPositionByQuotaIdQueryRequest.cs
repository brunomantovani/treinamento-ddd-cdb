using System;

namespace CdbContext.Application.Quotas.Queries.GetCurrentPositionByQuotaIdQuery
{
    public sealed record GetCurrentPositionByQuotaIdQueryRequest(
        Guid QuotaId);
}

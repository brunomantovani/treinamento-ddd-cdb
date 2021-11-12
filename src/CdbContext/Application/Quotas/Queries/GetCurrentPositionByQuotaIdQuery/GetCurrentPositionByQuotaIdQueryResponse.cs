using System;

namespace CdbContext.Application.Quotas.Queries.GetCurrentPositionByQuotaIdQuery
{
    public sealed record GetCurrentPositionByQuotaIdQueryResponse(
        decimal Amount,
        DateTime UpdatedAt);
}

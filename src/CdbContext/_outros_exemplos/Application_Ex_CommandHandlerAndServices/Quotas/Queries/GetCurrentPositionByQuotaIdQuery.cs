using System;

namespace CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Queries
{
    public sealed class GetCurrentPositionByQuotaIdQuery
    {
        public decimal Amount { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

using System;

namespace Api.Controllers.Quotas.ViewModels
{
    public sealed class PurchaseQuotaResponseViewModel
    {
        public decimal QuotaPositionAmount { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

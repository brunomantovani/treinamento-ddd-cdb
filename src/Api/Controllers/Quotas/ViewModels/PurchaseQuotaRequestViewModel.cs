using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Quotas.ViewModels
{
    public sealed class PurchaseQuotaRequestViewModel
    {
        [Required]
        public Guid? InvestmentAccountId { get; set; }

        [Required]
        public Guid? CheckingAccountId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal? Amount { get; set; }
    }
}
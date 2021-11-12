using System;

namespace CdbContext.Application.Commands.AddQuotaIncomeCommand
{
    public class AddQuotaIncomeCommandRequest
    {
        public Guid QuotaId { get; set; }
        public DateTime IncomeDate { get; set; }
        public decimal IncomeAmount { get; set; }
    }
}

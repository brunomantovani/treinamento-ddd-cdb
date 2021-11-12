using System;

namespace CdbContext.Application.Commands.AddQuotaIncomeCommand
{
    public sealed class AddQuotaIncomeCommandResponse
    {
        public decimal Amount { get; }
        public DateTime UpdatedAt { get; }
    }
}

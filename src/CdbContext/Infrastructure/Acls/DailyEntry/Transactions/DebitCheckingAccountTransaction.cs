using System;

namespace CdbContext.Infrastructure.Acls.DailyEntry.Transactions
{
    public sealed record DebitCheckingAccountTransaction(
        Guid CheckingAccountId,
        decimal Amount);
}

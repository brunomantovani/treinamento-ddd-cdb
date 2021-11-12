using System.Threading.Tasks;

namespace CdbContext.Infrastructure.Acls.DailyEntry
{
    public interface IDailyEntryAcl
    {
        Task Handle(Transactions.DebitCheckingAccountTransaction transaction);
    }
}

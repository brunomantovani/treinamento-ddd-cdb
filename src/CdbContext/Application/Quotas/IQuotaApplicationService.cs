using CdbContext.Application.Commands;
using CdbContext.DomainModels.Quotas;
using Commons.Infrastructure;
using System.Threading.Tasks;

namespace CdbContext.Application.Quotas
{
    public interface IQuotaApplicationService
        : ICommandHandler<PurchaseQuotaCommand, Task<Quota>>
    {
    }
}

using CdbContext.Application_Ex_Raiz.Quotas.Commands;
using CdbContext.DomainModels.Quotas;
using Commons.Infrastructure;
using System.Threading.Tasks;

namespace CdbContext.Application_Ex_Raiz.Quotas
{
    public interface IQuotaApplicationService
        : ICommandHandler<PurchaseQuotaCommand, Task<Quota>>
    {
    }
}

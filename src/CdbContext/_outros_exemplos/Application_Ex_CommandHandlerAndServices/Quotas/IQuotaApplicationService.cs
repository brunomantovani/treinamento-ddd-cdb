using CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Commands;
using CdbContext.DomainModels.Quotas;
using Commons.Infrastructure;
using System.Threading.Tasks;

namespace CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas
{
    public interface IQuotaApplicationService
        : ICommandHandler<PurchaseQuotaCommand, Task<Quota>>
        , ICommandHandler<RedeemQuotaCommand, Task<Quota>>
    {
    }
}

using CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Queries;
using Commons.Infrastructure;
using System.Threading.Tasks;

namespace CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas
{
    public interface IQuotaQueryService
        : IQueryHandler<GetCurrentPositionByQuotaIdQueryFilter, Task<GetCurrentPositionByQuotaIdQuery>>
    {
    }
}

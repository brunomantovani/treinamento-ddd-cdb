using CdbContext.Application.Quotas.Queries;
using System;
using System.Threading.Tasks;

namespace CdbContext.Application_Ex_Raiz.Quotas
{
    public interface IQuotaQueryService
    {
        Task<CurrentPositionData> GetCurrentPositionByQuotaIdQuery(Guid quotaId);
    }
}

using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.Infrastructure.Acls.JudicialBlock.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace CdbContext.Infrastructure.Acls.JudicialBlock
{
    public interface IJudicialBlockAcl
    {
        Task<GetBlockedValuesResponse> GetBlockedValuesAsync(InvestmentAccountId investmentAccounId, CancellationToken cancellationToken);
    }
}
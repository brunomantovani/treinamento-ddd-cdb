using CdbContext.Application.Quotas.Exceptions;
using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.DomainModels.Quotas;
using CdbContext.Infrastructure.Acls.JudicialBlock;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CdbContext.Application.Quotas.Commands.RedemptionQuotaCommand
{
    public sealed class RedemptionQuotaCommandHandler
    {
        private readonly IQuotaRepository _quotaRepository;
        private readonly IJudicialBlockAcl _judicialBlockAcl;

        public RedemptionQuotaCommandHandler(
            IQuotaRepository quotaRepository,
            IJudicialBlockAcl judicialBlockAcl)
        {
            _quotaRepository = quotaRepository;
            _judicialBlockAcl = judicialBlockAcl;
        }

        public async Task Handle(
            RedemptionQuotaCommandRequest request,
            CancellationToken cancellationToken)
        {
            var investmentAccountId = new InvestmentAccountId(
                request.InvestmentAccountId);

            var quotas = await _quotaRepository
                .GetAllQuotasAsync(investmentAccountId, cancellationToken);

            if (!quotas.Any())
            {
                throw new NotExistsQuotaToRedemptionException();
            }

            var blockedValues = await _judicialBlockAcl
                .GetBlockedValuesAsync(investmentAccountId, cancellationToken);

            var availableValue = quotas
                .Sum(quota => quota.GetPosition().Amount);

            if (blockedValues.Value >= availableValue)
            {
                throw new HasJudicialBlockToRedemptionException();
            }
        }
    }
}
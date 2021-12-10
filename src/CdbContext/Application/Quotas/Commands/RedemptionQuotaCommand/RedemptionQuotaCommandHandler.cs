using CdbContext.Application.Quotas.Exceptions;
using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.DomainModels.Quotas;
using CdbContext.DomainServices.SaleQuotasFromRedeemAmount;
using CdbContext.Infrastructure.Acls.JudicialBlock;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CdbContext.Application.Quotas.Commands.RedemptionQuotaCommand
{
    public sealed class RedemptionQuotaCommandHandler
    {
        private readonly IQuotaRepository _quotaRepository;
        private readonly IJudicialBlockAcl _judicialBlockAcl;
        private readonly ISaleQuotasFromRedeemAmountDomainService _saleQuotasFromRedeemAmountDomainService;

        public RedemptionQuotaCommandHandler(
            IQuotaRepository quotaRepository,
            IJudicialBlockAcl judicialBlockAcl,
            ISaleQuotasFromRedeemAmountDomainService saleQuotasFromRedeemAmountDomainService)
        {
            _quotaRepository = quotaRepository;
            _judicialBlockAcl = judicialBlockAcl;
            _saleQuotasFromRedeemAmountDomainService = saleQuotasFromRedeemAmountDomainService;
        }

        public async Task Handle(
            RedemptionQuotaCommandRequest request,
            CancellationToken cancellationToken)
        {
            var investmentAccountId = new InvestmentAccountId(
                request.InvestmentAccountId);

            var quotas = await GetQuotasToRedeemAsync(
                investmentAccountId,
                cancellationToken);

            await VerifyJudicialBlock(
                investmentAccountId,
                quotas,
                cancellationToken);

            var updatedQuotas = await _saleQuotasFromRedeemAmountDomainService
                .HandleAsync(quotas, cancellationToken);

            await _quotaRepository
                .UpdateRangeQuotasAsync(updatedQuotas, cancellationToken);
        }

        private async Task<IEnumerable<Quota>> GetQuotasToRedeemAsync(
            InvestmentAccountId investmentAccountId,
            CancellationToken cancellationToken)
        {
            var quotas = await _quotaRepository.GetAllQuotasAsync(
                investmentAccountId,
                cancellationToken);

            if (!quotas.Any())
            {
                throw new NotExistsQuotaToRedemptionException();
            }

            return quotas;
        }

        private async Task VerifyJudicialBlock(
            InvestmentAccountId investmentAccountId,
            IEnumerable<Quota> quotas,
            CancellationToken cancellationToken)
        {
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
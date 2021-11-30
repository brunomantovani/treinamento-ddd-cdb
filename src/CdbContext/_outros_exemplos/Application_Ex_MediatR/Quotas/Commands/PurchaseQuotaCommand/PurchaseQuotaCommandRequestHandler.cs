using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.DomainModels.Quotas;
using CdbContext.Infrastructure.Acls.DailyEntry;
using CdbContext.Infrastructure.Acls.DailyEntry.Transactions;
using Commons.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;

namespace CdbContext.Application_Ex_MediatR.Quotas.Commands.PurchaseQuotaCommand
{
    public sealed class PurchaseQuotaCommandRequestHandler
        : IRequestHandler<PurchaseQuotaCommandRequest, PurchaseQuotaCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuotaRepository _quotaRepository;
        private readonly IDailyEntryAcl _dailyEntryAcl;
        private readonly IMemoryCache _memoryCache;

        public PurchaseQuotaCommandRequestHandler(
            IUnitOfWork unitOfWork,
            IQuotaRepository quotaRepository,
            IDailyEntryAcl dailyEntryAcl,
            IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _quotaRepository = quotaRepository;
            _dailyEntryAcl = dailyEntryAcl;
            _memoryCache = memoryCache;
        }

        public async Task<PurchaseQuotaCommandResponse> Handle(
            PurchaseQuotaCommandRequest request,
            CancellationToken cancellationToken)
        {
            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                var investmentAccountId = new InvestmentAccountId(
                request.InvestmentAccountId);

                var quotaAmount = new QuotaAmount(
                    request.Amount);

                var quota = Quota.FromPurchase(
                    quotaAmount,
                    investmentAccountId);

                var debitCheckingAccountTransaction = new DebitCheckingAccountTransaction(
                    request.CheckingAccountId,
                    quotaAmount);

                await _quotaRepository
                    .AddAsync(quota, cancellationToken);

                var quotaPosition = quota.GetPosition();

                _memoryCache
                    .Set($"quota-position-{quota.Id}", quotaPosition);

                await _dailyEntryAcl
                    .Handle(debitCheckingAccountTransaction);

                transaction.Commit();

                //return quota;
                return new PurchaseQuotaCommandResponse(
                    quotaPosition.Amount,
                    quotaPosition.UpdatedAt);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}

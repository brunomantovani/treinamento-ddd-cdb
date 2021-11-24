using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.DomainModels.Quotas;
using CdbContext.Infrastructure.Acls.DailyEntry;
using CdbContext.Infrastructure.Acls.DailyEntry.Transactions;
using Commons.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;

namespace CdbContext.Application.Quotas.Commands.PurchaseQuotaCommand
{
    public sealed class PurchaseQuotaCommandRequestHandler
        : ICommandHandlerAsync<PurchaseQuotaCommandRequest, Quota>
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

        public async Task<Quota> HandleAsync(
            PurchaseQuotaCommandRequest command,
            CancellationToken cancellationToken)
        {
            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                var investmentAccountId = new InvestmentAccountId(
                command.InvestmentAccountId);

                var quotaAmount = new QuotaAmount(
                    command.Amount);

                var quota = new Quota(
                    quotaAmount,
                    investmentAccountId);

                var debitCheckingAccountTransaction = new DebitCheckingAccountTransaction(
                    command.CheckingAccountId,
                    quotaAmount);

                await _quotaRepository
                    .AddAsync(quota, cancellationToken);

                _memoryCache
                    .Set($"quota-position-{quota.Id}", quota.GetPosition());

                await _dailyEntryAcl
                    .Handle(debitCheckingAccountTransaction);

                transaction.Commit();

                return quota;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}

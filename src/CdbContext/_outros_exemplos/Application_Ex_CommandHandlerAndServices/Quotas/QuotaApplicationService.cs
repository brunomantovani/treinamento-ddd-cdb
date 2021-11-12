using CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Commands;
using CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Exceptions;
using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.DomainModels.Quotas;
using CdbContext.Infrastructure.Acls.DailyEntry;
using CdbContext.Infrastructure.Acls.DailyEntry.Transactions;
using Commons.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas
{
    public sealed class QuotaApplicationService
        : IQuotaApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuotaRepository _quotaRepository;
        private readonly IDailyEntryAcl _dailyEntryAcl;
        private readonly IMemoryCache _memoryCache;

        public QuotaApplicationService(
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

        public async Task<Quota> Handle(PurchaseQuotaCommand command)
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
                    .AddAsync(quota);

                _memoryCache
                    .Set($"quota-position-{quota.Id}", quota.GetPosition());

                await _dailyEntryAcl
                    .Handle(debitCheckingAccountTransaction);

                transaction.Commit();

                return quota;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new PurchaseQuotaCommandException($"Erro ao executar o {nameof(PurchaseQuotaCommand)}", ex);
            }
        }

        public Task<Quota> Handle(RedeemQuotaCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
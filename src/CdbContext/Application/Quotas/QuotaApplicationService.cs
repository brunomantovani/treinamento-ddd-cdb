using CdbContext.Application.Commands;
using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.DomainModels.Quotas;
using CdbContext.Infrastructure.Acls.DailyEntry;
using CdbContext.Infrastructure.Acls.DailyEntry.Transactions;
using Commons.Infrastructure;
using System;
using System.Threading.Tasks;

namespace CdbContext.Application.Quotas
{
    public sealed class QuotaApplicationService
        : IQuotaApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuotaRepository _quotaRepository;
        private readonly IDailyEntryAcl _dailyEntryAcl;

        public QuotaApplicationService(
            IUnitOfWork unitOfWork,
            IQuotaRepository quotaRepository,
            IDailyEntryAcl dailyEntryAcl)
        {
            _unitOfWork = unitOfWork;
            _quotaRepository = quotaRepository;
            _dailyEntryAcl = dailyEntryAcl;
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

                await _dailyEntryAcl
                    .Handle(debitCheckingAccountTransaction);

                transaction.Commit();

                return quota;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new PurchaseQuotaCommandException("", ex);
            }
        }
    }


    [Serializable]
    public class PurchaseQuotaCommandException : Exception
    {
        public PurchaseQuotaCommandException() { }
        public PurchaseQuotaCommandException(string message) : base(message) { }
        public PurchaseQuotaCommandException(string message, Exception inner) : base(message, inner) { }
        protected PurchaseQuotaCommandException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

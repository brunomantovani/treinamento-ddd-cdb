using CdbContext.DomainModels.InvestmentAccounts;
using Commons.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CdbContext.DomainModels.Quotas
{
    public sealed class Quota
        : AggregateRoot<QuotaId>
    {
        private readonly List<QuotaIncome> _incomeList;

        private Quota()
            : base(QuotaId.NewIdentity())
        {
            _incomeList = new List<QuotaIncome>();
        }

        public static Quota FromPurchase(
            QuotaAmount quotaAmount,
            InvestmentAccountId investmentAccountId)
        {
            if (quotaAmount <= 0)
            {
                throw new InvalidOperationException("o quota amount não pode menor ou igual a zero");
            }

            return new Quota
            {
                Amount = quotaAmount,
                InvestmentAccountId = investmentAccountId
            };
        }

        public QuotaAmount Amount { get; private set; }
        public InvestmentAccountId InvestmentAccountId { get; private set; }
        public IReadOnlyList<QuotaIncome> IncomeList
        {
            get
            {
                return _incomeList.AsReadOnly();
            }
        }

        public QuotaIncome AddIncome(
            DateTime incomeDate,
            decimal incomeAmount)
        {
            if (_incomeList.Any(x => x.Date.Date.Equals(incomeDate.Date)))
            {
                throw new Exception("Não é possível adicionar um rendimento, pois já existe um rendimento para esta data");
            }

            var quotaIncome = new QuotaIncome(
                incomeDate,
                incomeAmount);

            _incomeList.Add(quotaIncome);

            //poderia fazer
            // lançar um evento que a posição foi atualizada
            // retornar a posição já atualizada
            // returnar um tuple com o income e a posição

            return quotaIncome;
            //return new Tuple<QuotaIncome, QuotaPosition>(
            //    quotaIncome, GetPosition());
        }

        public QuotaPosition GetPosition()
        {
            var amount = Amount + _incomeList
                .Sum(x => x.Amount);

            return new QuotaPosition(
                amount,
                DateTime.Now);
        }

        public Quota Redeem(decimal redeemValue)
        {
            var redemQuotaAmount = new QuotaAmount(redeemValue * -1);

            var newValue = Amount + redemQuotaAmount;
            Amount = new QuotaAmount(newValue);

            return new Quota
            {
                Amount = redemQuotaAmount,
                InvestmentAccountId = InvestmentAccountId
            };
        }
    }
}

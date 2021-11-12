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

        public Quota(
            QuotaAmount quotaAmount,
            InvestmentAccountId investmentAccountId)
            : base(QuotaId.NewIdentity())
        {
            Amount = quotaAmount;
            InvestmentAccountId = investmentAccountId;

            _incomeList = new List<QuotaIncome>();
        }

        public QuotaAmount Amount { get; private set; }
        public InvestmentAccountId InvestmentAccountId { get; }
        public IReadOnlyList<QuotaIncome> IncomeList //=> _incomeList.AsReadOnly();
        {
            get
            {
                return _incomeList.AsReadOnly();
            }
        }

        public Quota Sell(decimal value)
        {
            var newValue = Amount - value;
            Amount = new QuotaAmount(newValue);

            return new Quota(Amount, InvestmentAccountId);
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

        //public QuotaPosition Position
        //{
        //    get
        //    {
        //        var amount = Amount + _incomeList
        //            .Sum(x => x.Amount);

        //        return new QuotaPosition(
        //            amount,
        //            DateTime.Now);
        //    }
        //}

        public QuotaPosition GetPosition()
        {
            var amount = Amount + _incomeList
                .Sum(x => x.Amount);

            return new QuotaPosition(
                amount,
                DateTime.Now);
        }

        //public override bool Equals(object obj)
        //{
        //    if(obj == null || obj.GetType() != GetType())
        //    {
        //        return false;
        //    }

        //    var quota = (Quota)obj;

        //    if(quota.Id == this.Id)
        //    {
        //        return true;
        //    }
        //}
    }
}

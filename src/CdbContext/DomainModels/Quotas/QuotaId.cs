using Commons.DomainModels;
using System;

namespace CdbContext.DomainModels.Quotas
{
    public sealed class QuotaId
        : Identity
    {
        public QuotaId(Guid value)
            : base(value)
        {
        }

        public static QuotaId NewIdentity()
        {
            return new QuotaId(Guid.NewGuid());
        }
    }
}

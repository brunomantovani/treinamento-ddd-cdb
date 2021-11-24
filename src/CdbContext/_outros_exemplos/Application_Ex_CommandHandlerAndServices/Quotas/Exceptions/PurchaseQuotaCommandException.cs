using System;

namespace CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Exceptions
{
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
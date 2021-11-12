using System;

namespace CdbContext._outros_exemplos.Application_Ex_CommandHandlerAndServices.Quotas.Exceptions
{
    [Serializable]
    public class GetCurrentPositionByQuotaIdQueryNotFoundException : Exception
    {
        public GetCurrentPositionByQuotaIdQueryNotFoundException() { }
        public GetCurrentPositionByQuotaIdQueryNotFoundException(string message) : base(message) { }
        public GetCurrentPositionByQuotaIdQueryNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected GetCurrentPositionByQuotaIdQueryNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
namespace CdbContext.Application.Quotas.Exceptions
{

    [System.Serializable]
    public class NotExistsQuotaToRedemptionException : System.Exception
    {
        public NotExistsQuotaToRedemptionException()
            : base("Não existe uma quota disponível para realizar o resgate")

        {
        }

        public NotExistsQuotaToRedemptionException(string message) : base(message) { }
        public NotExistsQuotaToRedemptionException(string message, System.Exception inner) : base(message, inner) { }
        protected NotExistsQuotaToRedemptionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
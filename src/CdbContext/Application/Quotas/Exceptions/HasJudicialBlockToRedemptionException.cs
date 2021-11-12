namespace CdbContext.Application.Quotas.Exceptions
{

    [System.Serializable]
    public class HasJudicialBlockToRedemptionException : System.Exception
    {
        public HasJudicialBlockToRedemptionException()
            : base("Este cliente possui valores bloqueados que ultrapassam a quantidade de investimentos")
        {
        }

        public HasJudicialBlockToRedemptionException(string message) : base(message) { }
        public HasJudicialBlockToRedemptionException(string message, System.Exception inner) : base(message, inner) { }
        protected HasJudicialBlockToRedemptionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
namespace Commons.Infrastructure
{
    public interface IQueryHandler<in TRequest, out TResponse>
    {
        TResponse Handle(TRequest request);
    }
}

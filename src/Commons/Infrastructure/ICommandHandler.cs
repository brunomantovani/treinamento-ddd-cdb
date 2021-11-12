namespace Commons.Infrastructure
{
    public interface ICommandHandler<in TRequest, out TResponse>
    {
        TResponse Handle(TRequest request);
    }
}
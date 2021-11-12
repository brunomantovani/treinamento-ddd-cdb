using System.Threading.Tasks;

namespace Commons.Infrastructure
{
    public interface ICommandHandler<in TCommand>
    {
        Task Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, out TResult>
    {
        TResult Handle(TCommand command);
    }
}

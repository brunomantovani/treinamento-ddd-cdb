namespace Commons.Infrastructure
{
    public interface IUnitOfWork
    {
        ITransaction BeginTransaction();
    }
}

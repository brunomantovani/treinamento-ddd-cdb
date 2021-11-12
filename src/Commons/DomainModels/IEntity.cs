namespace Commons.DomainModels
{
    public interface IEntity<TIdentity>
        where TIdentity : IIdentity
    {
        TIdentity Id { get; }
    }
}

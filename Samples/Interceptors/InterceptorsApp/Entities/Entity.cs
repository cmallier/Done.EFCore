namespace InterceptorsApp.Entities;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity( Guid id )
    {
        Id = id;
    }

    public Guid Id { get; init; }

    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    public void AddDomainEvent( IDomainEvent domainEvent )
    {
        _domainEvents.Add( domainEvent );
    }

    internal void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
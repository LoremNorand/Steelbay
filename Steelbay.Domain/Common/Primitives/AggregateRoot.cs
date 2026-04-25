using Steelbay.Domain.Common.Interfaces;




namespace Steelbay.Domain.Common.Primitives;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
{
    #region READONLY FIELDS

    private readonly List<IDomainEvent> _domainEvents = new();

    #endregion




    #region PUBLIC PROPERTIES

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    #endregion




    #region CONSTRUCTORS

    protected AggregateRoot(TId id) : base(id: id)
    { }

    #endregion




    #region PUBLIC METHODS

    public void ClearDomainEvents() => _domainEvents.Clear();

    #endregion




    #region PROTECTED METHODS

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(item: domainEvent);

    #endregion
}

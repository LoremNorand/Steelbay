using Steelbay.Domain.Common.Interfaces;
using Steelbay.Domain.Common.Utils;




namespace Steelbay.Domain.Common.DomainEvents;

public record DomainEvent : IDomainEvent
{
    #region INTERFACE IMPLEMENTATIONS

    #region IDomainEvent

    public EventId EventId { get; init; } = new(IdGenerator.NewId());
    public DateTime OccurredOnUtc { get; init; } = DateTime.UtcNow;

    #endregion

    #endregion
}

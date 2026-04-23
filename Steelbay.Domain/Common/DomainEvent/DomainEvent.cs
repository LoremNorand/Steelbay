using Steelbay.Domain.Common.Interfaces;
using Steelbay.Domain.Common.Util;




namespace Steelbay.Domain.Common.DomainEvent;

public record DomainEvent : IDomainEvent
{
    #region INTERFACE IMPLEMENTATIONS

    #region IDomainEvent

    public EventId EventId { get; init; } = new(IdGenerator.NewId());
    public DateTime OccurredOnUtc { get; init; } = DateTime.UtcNow;

    #endregion

    #endregion
}

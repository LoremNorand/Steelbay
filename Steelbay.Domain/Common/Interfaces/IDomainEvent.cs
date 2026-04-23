using MediatR;
using Steelbay.Domain.Common.DomainEvent;




namespace Steelbay.Domain.Common.Interfaces;

public interface IDomainEvent : INotification
{
    #region PUBLIC PROPERTIES

    public EventId EventId { get; }

    public DateTime OccurredOnUtc { get; }

    #endregion
}

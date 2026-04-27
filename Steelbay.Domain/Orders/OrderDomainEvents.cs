using Steelbay.Domain.Common.DomainEvents;
using Specification = Steelbay.Domain.Orders.TechnicalSpecifications.TechnicalSpecification;




namespace Steelbay.Domain.Orders;

public record OrderCreated(OrderId OrderId, string Code) : DomainEvent;

public record OrderStatusChanged(OrderId OrderId, OrderStatus OldStatus, OrderStatus NewStatus) : DomainEvent;

public record OrderSpecificationUpdated(OrderId OrderId, Specification Specification) : DomainEvent;

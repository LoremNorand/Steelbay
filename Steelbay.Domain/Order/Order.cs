using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Order;

public class Order : AggregateRoot<OrderId>
{
    #region PUBLIC PROPERTIES

    public string Code { get; private set; }
    public string Name { get; private set; }
    public OrderStatus Status { get; private set; }
    public string Type { get; private set; }

    #endregion




    #region CONSTRUCTORS

    private Order(OrderId id) : base(id)
    { }

    #endregion
}

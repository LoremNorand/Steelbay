using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Utils;
using Specification = Steelbay.Domain.Orders.TechnicalSpecifications.TechnicalSpecification;




namespace Steelbay.Domain.Orders;

public class Order : AggregateRoot<OrderId>
{
    #region PUBLIC PROPERTIES

    public string Code { get; private set; }
    public string Name { get; private set; }
    public Specification Specification { get; private set; }
    public OrderStatus Status { get; private set; }
    public string Type { get; private set; }

    #endregion




    #region CONSTRUCTORS

    private Order(OrderId id, string code, string name, string type, OrderStatus status, Specification specification) : base(id)
    {
        Code = code;
        Name = name;
        Type = type;
        Status = status;
        Specification = specification;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Order Create(string name, string type)
    {
        var id = OrderId.Generate();
        var code = CodeGenerator.Generate();
        var specification = Specification.Create();
        var status = OrderStatus.Draft;

        return new Order(id, code, name, type, status, specification);
    }

    #endregion
}

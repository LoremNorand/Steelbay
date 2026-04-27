using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Results;
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

#pragma warning disable CS8618
    // EF CORE Private constructor
    private Order() : base(id: null!)
    { }
#pragma warning restore CS8618


    private Order(OrderId id, string code, string name, string type, OrderStatus status, Specification specification) : base(id: id)
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

        var order = new Order(id: id, code: code, name: name, type: type, status: status, specification: specification);
        order.AddDomainEvent(domainEvent: new OrderCreated(OrderId: id, Code: code));

        return order;
    }

    #endregion




    #region PUBLIC METHODS

    public Result Confirm(IOrderPolicy policy) =>
        ChangeStatus(targetStatus: OrderStatus.Contracting, action: OrderAction.Confirm, policy: policy);


    public Result Cancel(IOrderPolicy policy) => ChangeStatus(targetStatus: OrderStatus.Cancelled, action: OrderAction.Cancel, policy: policy);


    public Result StartProcurement(IOrderPolicy policy) =>
        ChangeStatus(targetStatus: OrderStatus.Procurement, action: OrderAction.StartProcurement, policy: policy);


    public Result StartManufacturing(IOrderPolicy policy) =>
        ChangeStatus(targetStatus: OrderStatus.Manufacturing, action: OrderAction.StartManufacturing, policy: policy);


    public Result StartAssembly(IOrderPolicy policy) =>
        ChangeStatus(targetStatus: OrderStatus.Assembly, action: OrderAction.StartAssembly, policy: policy);


    public Result Complete(IOrderPolicy policy) => ChangeStatus(targetStatus: OrderStatus.Closed, action: OrderAction.Complete, policy: policy);


    public Result UpdateSpecification(Specification newSpecification, IOrderPolicy policy)
    {
        var checkResult = policy.CanExecute(order: this, action: OrderAction.UpdateSpecification);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "OrderId", value: Id));

        Specification = newSpecification;
        AddDomainEvent(domainEvent: new OrderSpecificationUpdated(OrderId: Id, Specification: newSpecification));

        return Result.Success();
    }

    #endregion




    #region PRIVATE METHODS

    private Result ChangeStatus(OrderStatus targetStatus, OrderAction action, IOrderPolicy policy)
    {
        if (Status == targetStatus) return Result.Success();

        var checkResult = policy.CanExecute(order: this, action: action);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "OrderId", value: Id));

        var oldStatus = Status;
        Status = targetStatus;

        AddDomainEvent(domainEvent: new OrderStatusChanged(OrderId: Id, OldStatus: oldStatus, NewStatus: targetStatus));

        return Result.Success();
    }

    #endregion
}

using Steelbay.Domain.Common.Results;




namespace Steelbay.Domain.Orders;

public static class OrderErrors
{
    #region PUBLIC STATIC METHODS

    public static Error ActionRestricted(OrderAction action, OrderStatus currentStatus)
    {
        var code = "order:action_restricted";
        var description = $"Action [{action}] is restricted for order in [{currentStatus}] status";

        return Error.Create(code: code, description: description);
    }

    #endregion
}

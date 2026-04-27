using Steelbay.Domain.Common.Results;




namespace Steelbay.Domain.Orders;

public interface IOrderPolicy
{
    Result CanExecute(Order order, OrderAction action);
}

using Steelbay.Domain.Common.Utils;




namespace Steelbay.Domain.Orders;

public record OrderId(Guid Value)
{
    #region PUBLIC STATIC METHODS

    public static OrderId Generate() => new(IdGenerator.NewId());

    #endregion
}

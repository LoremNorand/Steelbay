using Steelbay.Domain.Common.Util;




namespace Steelbay.Domain.Order;

public record OrderId(Guid Value)
{
    #region PUBLIC STATIC METHODS

    public static OrderId Generate() => new(IdGenerator.NewId());

    #endregion
}

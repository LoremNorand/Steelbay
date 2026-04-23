using Steelbay.Domain.Common.Util;




namespace Steelbay.Domain.Account;

public record AccountId(Guid Value)
{
    #region PUBLIC STATIC METHODS

    public static AccountId Generate() => new(IdGenerator.NewId());

    #endregion
}

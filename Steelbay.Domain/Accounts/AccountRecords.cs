using Steelbay.Domain.Common.Utils;




namespace Steelbay.Domain.Accounts;

public record AccountId(Guid Value)
{
    #region PUBLIC STATIC METHODS

    public static AccountId Generate() => new(Value: IdGenerator.NewId());

    #endregion
}

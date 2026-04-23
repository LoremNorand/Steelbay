using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.ValueObjects;

public sealed class PasswordHash : ValueObject
{
    #region PUBLIC PROPERTIES

    public string Value { get; private set; }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    #endregion
}

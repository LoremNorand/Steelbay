using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.ValueObjects;

public sealed class PasswordHash : ValueObject
{
    #region PUBLIC PROPERTIES

    public string Value { get; }

    #endregion




    #region OPERATORS

    public static implicit operator string(PasswordHash passwordHash) => passwordHash.Value;

    #endregion




    #region CONSTRUCTORS

    private PasswordHash()
    {
        Value = null!;
    }


    private PasswordHash(string value)
    {
        Value = value;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static PasswordHash Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
            throw new ArgumentException(message: "Password hash cannot be empty.", paramName: nameof(value));

        if (value.Any(predicate: char.IsWhiteSpace))
            throw new ArgumentException(message: "Password hash cannot contain whitespace.", paramName: nameof(value));

        return new PasswordHash(value: value);
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    #endregion
}

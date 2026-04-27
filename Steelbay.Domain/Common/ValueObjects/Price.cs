using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.ValueObjects;

public sealed class Price : ValueObject, IComparable<Price>, IComparable
{
    #region PUBLIC PROPERTIES

    public string Currency { get; }
    public double Value { get; }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IComparable<Price>

    public int CompareTo(Price? other)
    {
        ArgumentNullException.ThrowIfNull(argument: other);

        EnsureComparable(other: other);

        return Value.CompareTo(other.Value);
    }

    #endregion


    #region IComparable

    public int CompareTo(object? obj)
    {
        if (obj is not Price price)
            throw new ArgumentException(message: "Object must be a Price.", paramName: nameof(obj));

        return CompareTo(other: price);
    }

    #endregion

    #endregion




    #region CONSTRUCTORS

    private Price()
    {
        Currency = null!;
    }


    private Price(double value, string currency)
    {
        Value = value;
        Currency = currency;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Price Byn(double value) => new(value: ValidateValue(value: value), currency: "BYN");
    public static Price Usd(double value) => new(value: ValidateValue(value: value), currency: "USD");

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }

    #endregion




    #region PRIVATE METHODS

    private static double ValidateValue(double value)
    {
        if (double.IsNaN(d: value) || double.IsInfinity(d: value))
            throw new ArgumentOutOfRangeException(paramName: nameof(value), message: "Price must be a finite number.");

        if (value < 0)
            throw new ArgumentOutOfRangeException(paramName: nameof(value), message: "Price cannot be negative.");

        return value;
    }


    private void EnsureComparable(Price other)
    {
        if (!string.Equals(a: Currency, b: other.Currency, comparisonType: StringComparison.Ordinal))
            throw new InvalidOperationException(message: $"Cannot compare prices with different currencies: [{Currency}] and [{other.Currency}].");
    }

    #endregion
}

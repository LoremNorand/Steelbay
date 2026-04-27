using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.ValueObjects;

public class Price : ValueObject
{
    #region PUBLIC PROPERTIES

    public string Currency { get; }
    public double Value { get; }

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

    #endregion
}

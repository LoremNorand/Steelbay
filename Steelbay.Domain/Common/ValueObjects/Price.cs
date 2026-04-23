using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.ValueObjects;

public class Price : ValueObject
{
    #region PUBLIC PROPERTIES

    public string Currency { get; }
    public double Value { get; }

    #endregion




    #region CONSTRUCTORS

    private Price(double value, string currency)
    {
        Value = value;
        Currency = currency;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Price Byn(double value) => new(value, "BYN");
    public static Price Usd(double value) => new(value, "USD");

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }

    #endregion
}

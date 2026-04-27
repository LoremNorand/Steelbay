namespace Steelbay.Domain.Common.Rules;

public sealed class QuantityRule<T> : IRule<T>
{
    #region READONLY FIELDS

    private readonly int _quantity;
    private readonly Func<T, int> _valueSelector;

    #endregion




    #region CONSTRUCTORS

    public QuantityRule(Func<T, int> valueSelector, int quantity)
    {
        ArgumentNullException.ThrowIfNull(argument: valueSelector);

        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(paramName: nameof(quantity), actualValue: quantity,
                message: "Quantity must be greater than zero.");

        _valueSelector = valueSelector;
        _quantity = quantity;
    }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IRule<T>

    public bool IsSatisfiedBy(T value) => _valueSelector(arg: value) % _quantity == 0;

    #endregion

    #endregion
}

namespace Steelbay.Domain.Common.Rules;

public sealed class ComparisonRule<T, TValue> : IRule<T>
    where TValue : IComparable<TValue>
{
    #region READONLY FIELDS

    private readonly ComparisonOperator _comparisonOperator;
    private readonly TValue _expectedValue;
    private readonly Func<T, TValue> _valueSelector;

    #endregion




    #region CONSTRUCTORS

    public ComparisonRule(Func<T, TValue> valueSelector, TValue expectedValue, ComparisonOperator comparisonOperator)
    {
        ArgumentNullException.ThrowIfNull(argument: valueSelector);
        ArgumentNullException.ThrowIfNull(argument: expectedValue);

        _valueSelector = valueSelector;
        _expectedValue = expectedValue;
        _comparisonOperator = comparisonOperator;
    }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IRule<T>

    public bool IsSatisfiedBy(T value)
    {
        var actualValue = _valueSelector(arg: value);
        var comparisonResult = actualValue.CompareTo(_expectedValue);

        return _comparisonOperator switch
        {
            ComparisonOperator.GreaterThan => comparisonResult > 0,
            ComparisonOperator.GreaterThanOrEqual => comparisonResult >= 0,
            ComparisonOperator.LessThan => comparisonResult < 0,
            ComparisonOperator.LessThanOrEqual => comparisonResult <= 0,
            ComparisonOperator.Equal => comparisonResult == 0,
            ComparisonOperator.NotEqual => comparisonResult != 0,
            _ => throw new ArgumentOutOfRangeException(paramName: nameof(_comparisonOperator), actualValue: _comparisonOperator,
                message: "Unknown comparison operator.")
        };
    }

    #endregion

    #endregion
}




public enum ComparisonOperator
{
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Equal,
    NotEqual
}

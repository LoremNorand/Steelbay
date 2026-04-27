namespace Steelbay.Domain.Common.Rules;

public sealed class NotEqualRule<T, TValue> : IRule<T>
    where TValue : IComparable<TValue>
{
    #region READONLY FIELDS

    private readonly ComparisonRule<T, TValue> _innerRule;

    #endregion




    #region CONSTRUCTORS

    public NotEqualRule(Func<T, TValue> valueSelector, TValue expectedValue)
    {
        _innerRule = new ComparisonRule<T, TValue>(valueSelector: valueSelector, expectedValue: expectedValue,
            comparisonOperator: ComparisonOperator.NotEqual);
    }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IRule<T>

    public bool IsSatisfiedBy(T value) => _innerRule.IsSatisfiedBy(value: value);

    #endregion

    #endregion
}

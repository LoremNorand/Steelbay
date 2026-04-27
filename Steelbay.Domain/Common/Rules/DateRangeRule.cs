namespace Steelbay.Domain.Common.Rules;

public sealed class DateRangeRule<T> : IRule<T>
{
    #region READONLY FIELDS

    private readonly DateTime _endDate;
    private readonly Func<T, DateTime> _propertySelector;
    private readonly DateTime _startDate;

    #endregion




    #region CONSTRUCTORS

    public DateRangeRule(Func<T, DateTime> propertySelector, DateTime startDate, TimeSpan duration)
    {
        ArgumentNullException.ThrowIfNull(argument: propertySelector);

        if (duration < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(paramName: nameof(duration), actualValue: duration,
                message: "Date range duration cannot be negative.");

        _startDate = startDate;
        _propertySelector = propertySelector;
        _endDate = startDate + duration;
    }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IRule<T>

    public bool IsSatisfiedBy(T entity)
    {
        var value = _propertySelector(arg: entity);

        return value >= _startDate && value <= _endDate;
    }

    #endregion

    #endregion
}

namespace Steelbay.Domain.Common.Rules;

public sealed class InStateRule<T, TState> : IRule<T>
    where TState : struct, Enum
{
    #region READONLY FIELDS

    private readonly TState _expectedState;
    private readonly Func<T, TState> _stateSelector;

    #endregion




    #region CONSTRUCTORS

    public InStateRule(Func<T, TState> stateSelector, TState expectedState)
    {
        ArgumentNullException.ThrowIfNull(argument: stateSelector);

        if (!Enum.IsDefined(value: expectedState))
            throw new ArgumentOutOfRangeException(paramName: nameof(expectedState), actualValue: expectedState,
                message: "Unknown state value.");

        _stateSelector = stateSelector;
        _expectedState = expectedState;
    }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IRule<T>

    public bool IsSatisfiedBy(T value) => EqualityComparer<TState>.Default.Equals(x: _stateSelector(arg: value), y: _expectedState);

    #endregion

    #endregion
}

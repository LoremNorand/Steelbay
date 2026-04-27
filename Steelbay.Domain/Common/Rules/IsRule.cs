namespace Steelbay.Domain.Common.Rules;

public sealed class IsRule<T> : IRule<T>
{
    #region READONLY FIELDS

    private readonly Func<T, bool> _predicate;

    #endregion




    #region CONSTRUCTORS

    public IsRule(Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(argument: predicate);

        _predicate = predicate;
    }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IRule<T>

    public bool IsSatisfiedBy(T value) => _predicate(arg: value);

    #endregion

    #endregion
}

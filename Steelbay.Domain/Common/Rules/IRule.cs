namespace Steelbay.Domain.Common.Rules;

public interface IRule<T>
{
    #region PUBLIC METHODS

    bool IsSatisfiedBy(T value);

    #endregion
}

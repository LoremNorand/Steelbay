namespace Steelbay.Domain.Common.Rules;

public sealed class OrRule<T> : IRule<T>
{
    #region READONLY FIELDS

    private readonly IReadOnlyCollection<IRule<T>> _rules;

    #endregion




    #region CONSTRUCTORS

    public OrRule(params IRule<T>[] rules)
    {
        ArgumentNullException.ThrowIfNull(argument: rules);

        if (rules.Length < 2)
            throw new ArgumentException(message: "OrRule requires at least two inner rules.", paramName: nameof(rules));

        if (rules.Any(rule => rule is null))
            throw new ArgumentException(message: "OrRule cannot contain null rules.", paramName: nameof(rules));

        _rules = rules.ToArray();
    }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IRule<T>

    public bool IsSatisfiedBy(T value) => _rules.Any(predicate: rule => rule.IsSatisfiedBy(value: value));

    #endregion

    #endregion
}

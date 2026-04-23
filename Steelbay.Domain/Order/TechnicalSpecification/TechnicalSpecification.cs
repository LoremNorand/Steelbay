using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Result;




namespace Steelbay.Domain.Order.TechnicalSpecification;

public class TechnicalSpecification : Entity<Guid>
{
    #region FIELDS

    private Dictionary<string, TechnicalSpecificationValue> _values;

    #endregion




    #region PUBLIC PROPERTIES

    public IReadOnlyDictionary<string, TechnicalSpecificationValue> Values => _values.AsReadOnly();

    #endregion




    #region CONSTRUCTORS

    private TechnicalSpecification(Guid id) : base(id)
    { }

    #endregion




    #region PUBLIC METHODS

    public Result Set<T>(string key, T value) where T : notnull
    {
        if (_values.ContainsKey(key))
            _values.Remove(key);

        _values.Add(key, TechnicalSpecificationValue.Set(value));

        return Result.Success();
    }

    #endregion
}

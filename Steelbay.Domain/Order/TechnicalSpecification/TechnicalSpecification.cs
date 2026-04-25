using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Result;
using Steelbay.Domain.Common.Util;




namespace Steelbay.Domain.Order.TechnicalSpecification;

public class TechnicalSpecification : Entity<Guid>
{
    #region READONLY FIELDS

    private readonly Dictionary<string, TechnicalSpecificationValue> _values;

    #endregion




    #region PUBLIC PROPERTIES

    public IReadOnlyDictionary<string, TechnicalSpecificationValue> Values => _values.AsReadOnly();

    #endregion




    #region CONSTRUCTORS

    private TechnicalSpecification(Guid id, Dictionary<string, TechnicalSpecificationValue> values) : base(id)
    {
        _values = values;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static TechnicalSpecification Create(Dictionary<string, TechnicalSpecificationValue>? values = null)
    {
        var valuesDictionary = values ?? new Dictionary<string, TechnicalSpecificationValue>();
        var id = IdGenerator.NewId();

        return new TechnicalSpecification(id, valuesDictionary);
    }

    #endregion




    #region PUBLIC METHODS

    public Result Set<T>(string key, T value) where T : notnull
    {
        _values.Remove(key);

        _values.Add(key, TechnicalSpecificationValue.Set(value));

        return Result.Success();
    }

    #endregion
}

using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Results;
using Steelbay.Domain.Common.Utils;




namespace Steelbay.Domain.Orders.TechnicalSpecifications;

public class TechnicalSpecification : Entity<Guid>
{
    #region READONLY FIELDS

    private readonly Dictionary<string, TechnicalSpecificationValue> _values;

    #endregion




    #region PUBLIC PROPERTIES

    public IReadOnlyDictionary<string, TechnicalSpecificationValue> Values => _values.AsReadOnly();

    #endregion




    #region CONSTRUCTORS

    private TechnicalSpecification(Guid id, Dictionary<string, TechnicalSpecificationValue> values) : base(id: id)
    {
        _values = values;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static TechnicalSpecification Create(Dictionary<string, TechnicalSpecificationValue>? values = null)
    {
        var valuesDictionary = values ?? new Dictionary<string, TechnicalSpecificationValue>();
        var id = IdGenerator.NewId();

        return new TechnicalSpecification(id: id, values: valuesDictionary);
    }

    #endregion




    #region PUBLIC METHODS

    public Result Set<T>(string key, T value) where T : notnull
    {
        _values.Remove(key: key);

        _values.Add(key: key, value: TechnicalSpecificationValue.Set(value: value));

        return Result.Success();
    }

    #endregion
}

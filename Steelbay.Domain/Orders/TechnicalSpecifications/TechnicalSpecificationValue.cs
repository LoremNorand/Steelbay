using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Orders.TechnicalSpecifications;

public class TechnicalSpecificationValue : ValueObject
{
    #region READONLY FIELDS

    private readonly object _value;

    #endregion




    #region PUBLIC PROPERTIES

    public Type Type { get; }

    #endregion




    #region CONSTRUCTORS

    private TechnicalSpecificationValue(object value, Type type)
    {
        _value = value;
        Type = type;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static TechnicalSpecificationValue Set<T>(T value) where T : notnull => new(value, typeof(T));

    #endregion




    #region PUBLIC METHODS

    public T As<T>() => (T)_value;

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
        yield return Type;
    }

    #endregion
}

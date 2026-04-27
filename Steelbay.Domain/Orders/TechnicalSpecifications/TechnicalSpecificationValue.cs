using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Orders.TechnicalSpecifications;

public sealed class TechnicalSpecificationValue : ValueObject
{
    #region READONLY FIELDS

    private static readonly HashSet<Type> _supportedTypes =
    [
        typeof(string),
        typeof(double),
        typeof(int),
        typeof(bool)
    ];

    private object _value;

    #endregion




    #region PUBLIC PROPERTIES

    public Type Type { get; }

    #endregion




    #region CONSTRUCTORS

    private TechnicalSpecificationValue()
    {
        _value = null!;
        Type = null!;
    }


    private TechnicalSpecificationValue(object value, Type type)
    {
        _value = value;
        Type = type;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static TechnicalSpecificationValue Set<T>(T value) where T : notnull
    {
        var type = typeof(T);

        if (!_supportedTypes.Contains(item: type))
            throw new ArgumentException(message: $"Technical specification value type [{type.Name}] is not supported.",
                paramName: nameof(value));

        return new TechnicalSpecificationValue(value: value, type: type);
    }

    #endregion




    #region PUBLIC METHODS

    public T As<T>()
    {
        if (typeof(T) != Type)
            throw new InvalidCastException(message: $"Stored value type is [{Type.Name}], requested [{typeof(T).Name}].");

        return (T)_value;
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _value;
        yield return Type;
    }

    #endregion
}

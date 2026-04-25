using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.Results;

public class Error : ValueObject
{
    #region READONLY FIELDS

    private readonly Dictionary<string, object> _metadata;

    #endregion




    #region PUBLIC PROPERTIES

    public string Code { get; }
    public string Description { get; }
    public IReadOnlyDictionary<string, object> Metadata => _metadata.AsReadOnly();

    public static Error None => Create(code: string.Empty, description: string.Empty);

    #endregion




    #region CONSTRUCTORS

    private Error(string code, string description, Dictionary<string, object>? metadata = null)
    {
        Code = code;
        Description = description;
        _metadata = metadata ?? new Dictionary<string, object>();
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Error Create(string code, string description, Dictionary<string, object>? metadata = null) =>
        new(code: code, description: description, metadata: metadata);

    #endregion




    #region PUBLIC METHODS

    public Error WithContext(string key, object value)
    {
        var newMetadata = new Dictionary<string, object>(dictionary: _metadata) { [key: key] = value };

        return Create(code: Code, description: Description, metadata: newMetadata);
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return Description;
        yield return Metadata;
    }

    #endregion
}

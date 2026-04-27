using System.Collections.ObjectModel;
using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.Results;

public sealed class Error : ValueObject
{
    #region READONLY FIELDS

    private readonly Dictionary<string, object?> _metadata;

    #endregion




    #region PUBLIC PROPERTIES

    public string Code { get; }
    public string Description { get; }
    public IReadOnlyDictionary<string, object?> Metadata => new ReadOnlyDictionary<string, object?>(dictionary: _metadata);

    public static Error None => Create(code: string.Empty, description: string.Empty);

    #endregion




    #region CONSTRUCTORS

    private Error()
    {
        Code = null!;
        Description = null!;
        _metadata = new Dictionary<string, object?>();
    }


    private Error(string code, string description, Dictionary<string, object?>? metadata = null)
    {
        Code = code;
        Description = description;

        _metadata = metadata is null
            ? new Dictionary<string, object?>()
            : new Dictionary<string, object?>(dictionary: metadata);
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Error Create(string code, string description, Dictionary<string, object?>? metadata = null) =>
        new(code: code, description: description, metadata: metadata);

    #endregion




    #region PUBLIC METHODS

    public Error WithContext(string key, object? value)
    {
        if (string.IsNullOrWhiteSpace(value: key))
            throw new ArgumentException(message: "Context key cannot be empty.", paramName: nameof(key));

        var newMetadata = new Dictionary<string, object?>(dictionary: _metadata)
        {
            [key: key] = value
        };

        return Create(code: Code, description: Description, metadata: newMetadata);
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Code;
        yield return Description;

        foreach (var pair in _metadata.OrderBy(keySelector: pair => pair.Key, comparer: StringComparer.Ordinal))
        {
            yield return pair.Key;
            yield return pair.Value;
        }
    }

    #endregion
}

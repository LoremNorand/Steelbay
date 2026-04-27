using System.Text.RegularExpressions;
using Steelbay.Domain.Common.Exceptions;
using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.ValueObjects.Documents;

public class Key : ValueObject
{
    #region READONLY FIELDS

    private static readonly Regex _extensionRegex = new(
        pattern: @"(?<extension>\.[A-Za-z0-9]+)$",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase);

    #endregion




    #region PUBLIC PROPERTIES

    public string Extension { get; }
    public KeyType KeyType { get; }
    public string Value { get; }

    #endregion




    #region CONSTRUCTORS

    private Key()
    {
        Extension = null!;
        Value = null!;
    }


    private Key(string value, string extension, KeyType keyType)
    {
        Value = value;
        Extension = extension;
        KeyType = keyType;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Key CreateDocument(string key)
    {
        var normalizedKey = NormalizeKey(key: key);
        var extension = GetExtension(key: normalizedKey);

        if (!ExtensionWhitelist.ForDocument.Contains(item: extension))
            throw new ArgumentException(message: $"Unsupported document extension: {extension}", paramName: nameof(key));

        return new Key(value: normalizedKey, extension: extension, keyType: KeyType.Document);
    }


    public static Key CreateImage(string key)
    {
        var normalizedKey = NormalizeKey(key: key);
        var extension = GetExtension(key: normalizedKey);

        if (!ExtensionWhitelist.ForImage.Contains(item: extension))
            throw new ArgumentException(message: $"Unsupported image extension: {extension}", paramName: nameof(key));

        return new Key(value: normalizedKey, extension: extension, keyType: KeyType.Image);
    }


    public static Key CreateVideo(string key)
    {
        var normalizedKey = NormalizeKey(key: key);
        var extension = GetExtension(key: normalizedKey);

        if (!ExtensionWhitelist.ForVideo.Contains(item: extension))
            throw new ArgumentException(message: $"Unsupported video extension: {extension}", paramName: nameof(key));

        return new Key(value: normalizedKey, extension: extension, keyType: KeyType.Video);
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Extension;
        yield return KeyType;
    }

    #endregion




    #region PRIVATE METHODS

    private static string GetExtension(string key)
    {
        var match = _extensionRegex.Match(input: key);

        if (match.Success) return match.Groups[groupname: "extension"].Value.ToLowerInvariant();

        throw new EmptyKeyException(key: key);
    }


    private static string NormalizeKey(string key)
    {
        if (string.IsNullOrWhiteSpace(value: key))
            throw new ArgumentException(message: "Key cannot be empty.", paramName: nameof(key));

        return key.Trim();
    }

    #endregion
}

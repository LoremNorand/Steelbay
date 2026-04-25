using System.Text.RegularExpressions;
using Steelbay.Domain.Common.Exceptions;
using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.ValueObjects.Documents;

public class Key : ValueObject
{
    #region PUBLIC PROPERTIES

    public string Extension { get; }
    public KeyType KeyType { get; }
    public string Value { get; }

    #endregion




    #region CONSTRUCTORS

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
        var extension = GetExtension(key: key);

        if (!ExtensionWhitelist.ForDocument.Contains(item: extension))
            throw new ArgumentException();

        return new Key(value: key, extension: extension, keyType: KeyType.Document);
    }


    public static Key CreateImage(string key)
    {
        var extension = GetExtension(key: key);

        if (!ExtensionWhitelist.ForImage.Contains(item: extension))
            throw new ArgumentException();

        return new Key(value: key, extension: extension, keyType: KeyType.Image);
    }


    public static Key CreateVideo(string key)
    {
        var extension = GetExtension(key: key);

        if (!ExtensionWhitelist.ForVideo.Contains(item: extension))
            throw new ArgumentException();

        return new Key(value: key, extension: extension, keyType: KeyType.Video);
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Extension;
    }

    #endregion




    #region PRIVATE METHODS

    private static string GetExtension(string key)
    {
        var reg = new Regex(pattern: @"(\.\w+)\s*^", options: RegexOptions.IgnoreCase | RegexOptions.Compiled);
        var match = reg.Match(input: key);

        if (match.Success) return match.Groups[groupnum: 1].Value;

        throw new EmptyKeyException(key: key);
    }

    #endregion
}

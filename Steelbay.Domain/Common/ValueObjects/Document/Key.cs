using System.Text.RegularExpressions;
using Steelbay.Domain.Common.Exception;
using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.ValueObjects.Document;

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
        var extension = GetExtension(key);

        if (!ExtensionWhitelist.ForDocument.Contains(extension))
            throw new ArgumentException();

        return new Key(key, extension, KeyType.Document);
    }


    public static Key CreateImage(string key)
    {
        var extension = GetExtension(key);

        if (!ExtensionWhitelist.ForImage.Contains(extension))
            throw new ArgumentException();

        return new Key(key, extension, KeyType.Image);
    }


    public static Key CreateVideo(string key)
    {
        var extension = GetExtension(key);

        if (!ExtensionWhitelist.ForVideo.Contains(extension))
            throw new ArgumentException();

        return new Key(key, extension, KeyType.Video);
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
        var reg = new Regex(@"(\.\w+)\s*^", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        var match = reg.Match(key);

        if (match.Success) return match.Groups[1].Value;

        throw new EmptyKeyException(key);
    }

    #endregion
}

namespace Steelbay.Domain.Common.Exception;

public class EmptyKeyException : DomainException
{
    #region PUBLIC PROPERTIES

    public string Key { get; }

    #endregion




    #region CONSTRUCTORS

    public EmptyKeyException(string key) : base($"Empty document path: [{key}]")
    {
        Key = key;
    }

    #endregion
}

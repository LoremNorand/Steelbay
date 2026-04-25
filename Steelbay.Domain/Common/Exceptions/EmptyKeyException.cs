namespace Steelbay.Domain.Common.Exceptions;

public class EmptyKeyException : DomainException
{
    #region PUBLIC PROPERTIES

    public string Key { get; }

    #endregion




    #region CONSTRUCTORS

    public EmptyKeyException(string key) : base(message: $"Empty document path: [{key}]")
    {
        Key = key;
    }

    #endregion
}

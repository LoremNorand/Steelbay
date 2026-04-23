namespace Steelbay.Domain.Common.Exception;

public class DomainException : System.Exception
{
    #region CONSTRUCTORS

    protected DomainException(string message) : base(message)
    { }

    #endregion
}

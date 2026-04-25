namespace Steelbay.Domain.Common.Exceptions;

public class DomainException : System.Exception
{
    #region CONSTRUCTORS

    protected DomainException(string message) : base(message)
    { }

    #endregion
}

namespace Steelbay.Domain.Common.Exceptions;

public class DomainException : Exception
{
    #region CONSTRUCTORS

    protected DomainException(string message) : base(message: message)
    { }

    #endregion
}

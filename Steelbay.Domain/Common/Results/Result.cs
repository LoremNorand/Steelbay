namespace Steelbay.Domain.Common.Results;

public class Result
{
    #region PUBLIC PROPERTIES

    public Error Error { get; }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    #endregion




    #region CONSTRUCTORS

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Result Failure(Error error) => new(isSuccess: false, error: error);

    public static Result Success() => new(isSuccess: true, error: Error.None);

    #endregion
}

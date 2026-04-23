using Steelbay.Domain.Common.History;
using Steelbay.Domain.Common.ValueObjects;




namespace Steelbay.Domain.Account.AccountStatus;

public class AccountStatusCommit : HistoryCommit
{
    #region PUBLIC PROPERTIES

    public Reason Reason { get; }
    public AccountStatus Status { get; }

    #endregion




    #region CONSTRUCTORS

    protected AccountStatusCommit(DateTime createdAtUtc, AccountStatus status, Reason reason)
    {
        CreatedAtUtc = createdAtUtc;
        Status = status;
        Reason = reason;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static AccountStatusCommit Create(AccountStatus status, Reason reason)
    {
        var timestampUtc = DateTime.UtcNow;
        var handledReason = reason;

        return new AccountStatusCommit(timestampUtc, status, handledReason);
    }

    #endregion




    #region PUBLIC METHODS

    public override object ToMetadata() => new
    {
        Status = Status.ToString(),
        ChangeReason = Reason,
        CreatedAtUtc
    };

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Status;
        yield return CreatedAtUtc;
        yield return Reason;
    }

    #endregion
}

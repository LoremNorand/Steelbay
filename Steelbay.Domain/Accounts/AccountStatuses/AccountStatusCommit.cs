using Steelbay.Domain.Common.Histories;
using Steelbay.Domain.Common.ValueObjects;




namespace Steelbay.Domain.Accounts.AccountStatuses;

public class AccountStatusCommit : HistoryCommit
{
    #region PUBLIC PROPERTIES

    public Reason Reason { get; }
    public AccountStatus Status { get; }

    #endregion




    #region CONSTRUCTORS

    private AccountStatusCommit()
    {
        Reason = null!;
    }


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
        if (!Enum.IsDefined(value: status))
            throw new ArgumentOutOfRangeException(paramName: nameof(status), actualValue: status, message: "Unknown account status.");

        if (reason is null)
            throw new ArgumentNullException(paramName: nameof(reason));

        var timestampUtc = DateTime.UtcNow;
        var handledReason = reason;

        return new AccountStatusCommit(createdAtUtc: timestampUtc, status: status, reason: handledReason);
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

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Status;
        yield return CreatedAtUtc;
        yield return Reason;
    }

    #endregion
}

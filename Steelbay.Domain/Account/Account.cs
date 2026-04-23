using Steelbay.Domain.Account.AccountStatus;
using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Result;
using Steelbay.Domain.Common.ValueObjects;




namespace Steelbay.Domain.Account;

public class Account : AggregateRoot<AccountId>
{
    #region PUBLIC PROPERTIES

    public Email Email { get; private set; }

    public AccountStatusHistory StatusHistory { get; private set; }

    #endregion




    #region CONSTRUCTORS

    private Account(AccountId id, Email email) : base(id)
    {
        Email = email;
        AddDomainEvent(new AccountCreated(id));
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Account Create(Email email)
    {
        var id = AccountId.Generate();
        var account = new Account(id, email);

        return account;
    }

    #endregion




    #region PUBLIC METHODS

    public Result Ban(Reason reason, IAccountStatusPolicy policy)
    {
        var result = ChangeStatus(reason, policy, AccountStatus.AccountStatus.Banned);

        if (result.IsSuccess)
            AddDomainEvent(new AccountBanned(Id));

        return result;
    }


    public Result Delete(Reason reason, IAccountStatusPolicy policy)
    {
        var result = ChangeStatus(reason, policy, AccountStatus.AccountStatus.Deleted);

        if (result.IsSuccess)
            AddDomainEvent(new AccountDeleted(Id));

        return result;
    }


    public Result Restore(Reason reason, IAccountStatusPolicy policy)
    {
        var result = ChangeStatus(reason, policy, AccountStatus.AccountStatus.Active);

        if (result.IsSuccess)
            AddDomainEvent(new AccountRestored(Id));

        return result;
    }


    public Result Suspect(Reason reason, IAccountStatusPolicy policy)
    {
        var result = ChangeStatus(reason, policy, AccountStatus.AccountStatus.Suspicious);

        if (result.IsSuccess)
            AddDomainEvent(new AccountSuspected(Id));

        return result;
    }


    public Result UpdateEmail(Email newEmail, IAccountStatusPolicy policy)
    {
        if (StatusHistory.Current == null)
            return Result.Failure(AccountErrors.CurrentStatusIsNull());

        var checkResult = policy.CanExecute(StatusHistory.Current.Status, AccountAction.UpdateEmail);

        if (checkResult.IsFailure)
            return Result.Failure(checkResult.Error.WithContext("AccountId", Id));

        var oldEmail = Email;
        Email = newEmail;

        AddDomainEvent(new AccountEmailUpdated(Id, oldEmail, newEmail));

        return Result.Success();
    }

    #endregion




    #region PRIVATE METHODS

    private Result ChangeStatus(Reason reason, IAccountStatusPolicy policy, AccountStatus.AccountStatus targetStatus)
    {
        if (StatusHistory.Current == null)
            return Result.Failure(AccountErrors.CurrentStatusIsNull());

        var checkResult = policy.CanTransit(StatusHistory.Current.Status, targetStatus);

        if (checkResult.IsFailure)
            return Result.Failure(checkResult.Error.WithContext("AccountId", Id));

        StatusHistory.Commit(AccountStatusCommit.Create(targetStatus, reason));

        return Result.Success();
    }

    #endregion
}

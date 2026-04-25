using Steelbay.Domain.Accounts.AccountStatuses;
using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Results;
using Steelbay.Domain.Common.ValueObjects;




namespace Steelbay.Domain.Accounts;

public class Account : AggregateRoot<AccountId>
{
    #region PUBLIC PROPERTIES

    public AccountStatus CurrentStatus { get; private set; }

    public Email Email { get; private set; }

    #endregion




    #region CONSTRUCTORS

    private Account(AccountId id, Email email) : base(id)
    {
        Email = email;
        CurrentStatus = AccountStatus.Active;
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
        var changedStatus = CurrentStatus;
        var result = ChangeStatus(reason, policy, AccountStatus.Banned);

        if (result.IsSuccess)
            AddDomainEvent(new AccountBanned(Id, changedStatus));

        return result;
    }


    public Result Delete(Reason reason, IAccountStatusPolicy policy)
    {
        var changedStatus = CurrentStatus;
        var result = ChangeStatus(reason, policy, AccountStatus.Deleted);

        if (result.IsSuccess)
            AddDomainEvent(new AccountDeleted(Id, changedStatus));

        return result;
    }


    public Result Restore(Reason reason, IAccountStatusPolicy policy)
    {
        var changedStatus = CurrentStatus;
        var result = ChangeStatus(reason, policy, AccountStatus.Active);

        if (result.IsSuccess)
            AddDomainEvent(new AccountRestored(Id, changedStatus));

        return result;
    }


    public Result Suspect(Reason reason, IAccountStatusPolicy policy)
    {
        var changedStatus = CurrentStatus;
        var result = ChangeStatus(reason, policy, AccountStatus.Suspicious);

        if (result.IsSuccess)
            AddDomainEvent(new AccountSuspected(Id, changedStatus));

        return result;
    }


    public Result UpdateEmail(Email newEmail, IAccountStatusPolicy policy)
    {
        var checkResult = policy.CanExecute(CurrentStatus, AccountAction.UpdateEmail);

        if (checkResult.IsFailure)
            return Result.Failure(checkResult.Error.WithContext("AccountId", Id));

        var oldEmail = Email;
        Email = newEmail;

        AddDomainEvent(new AccountEmailUpdated(Id, oldEmail, newEmail));

        return Result.Success();
    }

    #endregion




    #region PRIVATE METHODS

    private Result ChangeStatus(Reason reason, IAccountStatusPolicy policy, AccountStatus targetStatus)
    {
        var checkResult = policy.CanTransit(CurrentStatus, targetStatus);

        if (checkResult.IsFailure)
            return Result.Failure(checkResult.Error.WithContext("AccountId", Id));

        CurrentStatus = targetStatus;

        return Result.Success();
    }

    #endregion
}

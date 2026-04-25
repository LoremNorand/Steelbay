using Steelbay.Domain.Accounts.AccountStatuses;
using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Results;
using Steelbay.Domain.Common.ValueObjects;




namespace Steelbay.Domain.Accounts;

public class Account : AggregateRoot<AccountId>
{
    #region PUBLIC PROPERTIES

    public AccountStatus CurrentStatus { get; private set; }

    public string DisplayName { get; private set; }

    public Email Email { get; private set; }

    public PasswordHash PasswordHash { get; private set; }

    #endregion




    #region CONSTRUCTORS

#pragma warning disable CS8618
    // EF CORE Private constructor
    private Account() : base(id: null!)
    { }
#pragma warning restore CS8618


    private Account(AccountId id, Email email, PasswordHash passwordHash, string displayName) : base(id: id)
    {
        Email = email;
        CurrentStatus = AccountStatus.Active;
        PasswordHash = passwordHash;
        DisplayName = displayName;
        AddDomainEvent(domainEvent: new AccountCreated(AccountId: id));
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Account Create(Email email, string displayName, PasswordHash passwordHash)
    {
        var id = AccountId.Generate();
        var account = new Account(id: id, email: email, passwordHash: passwordHash, displayName: displayName);

        return account;
    }

    #endregion




    #region PUBLIC METHODS

    public Result Ban(Reason reason, IAccountStatusPolicy policy)
    {
        var changedStatus = CurrentStatus;
        var result = ChangeStatus(reason: reason, policy: policy, targetStatus: AccountStatus.Banned);

        if (result.IsSuccess)
            AddDomainEvent(domainEvent: new AccountBanned(AccountId: Id, ChangedStatus: changedStatus));

        return result;
    }


    public Result Delete(Reason reason, IAccountStatusPolicy policy)
    {
        var changedStatus = CurrentStatus;
        var result = ChangeStatus(reason: reason, policy: policy, targetStatus: AccountStatus.Deleted);

        if (result.IsSuccess)
            AddDomainEvent(domainEvent: new AccountDeleted(AccountId: Id, ChangedStatus: changedStatus));

        return result;
    }


    public Result Restore(Reason reason, IAccountStatusPolicy policy)
    {
        var changedStatus = CurrentStatus;
        var result = ChangeStatus(reason: reason, policy: policy, targetStatus: AccountStatus.Active);

        if (result.IsSuccess)
            AddDomainEvent(domainEvent: new AccountRestored(AccountId: Id, ChangedStatus: changedStatus));

        return result;
    }


    public Result Suspect(Reason reason, IAccountStatusPolicy policy)
    {
        var changedStatus = CurrentStatus;
        var result = ChangeStatus(reason: reason, policy: policy, targetStatus: AccountStatus.Suspicious);

        if (result.IsSuccess)
            AddDomainEvent(domainEvent: new AccountSuspected(AccountId: Id, ChangedStatus: changedStatus));

        return result;
    }


    public Result UpdateDisplayName(string newDisplayName, IAccountStatusPolicy policy)
    {
        var oldDisplayName = DisplayName;
        var checkResult = policy.CanExecute(current: CurrentStatus, action: AccountAction.UpdateDisplayName);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "AccountId", value: Id));

        DisplayName = newDisplayName;

        AddDomainEvent(
            domainEvent: new AccountDisplayNameUpdated(AccountId: Id, OldDisplayName: oldDisplayName, NewDisplayName: newDisplayName));

        return Result.Success();
    }


    public Result UpdateEmail(Email newEmail, IAccountStatusPolicy policy)
    {
        var oldEmail = Email;
        var checkResult = policy.CanExecute(current: CurrentStatus, action: AccountAction.UpdateEmail);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "AccountId", value: Id));

        Email = newEmail;

        AddDomainEvent(domainEvent: new AccountEmailUpdated(AccountId: Id, OldEmail: oldEmail, NewEmail: newEmail));

        return Result.Success();
    }


    public Result UpdatePassword(PasswordHash newPasswordHash, IAccountStatusPolicy policy)
    {
        var changedPassword = newPasswordHash;
        var checkResult = policy.CanExecute(current: CurrentStatus, action: AccountAction.UpdatePassword);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "AccountId", value: Id));

        PasswordHash = newPasswordHash;

        AddDomainEvent(domainEvent: new AccountPasswordUpdated(AccountId: Id, OldPassword: changedPassword, NewPassword: newPasswordHash));

        return Result.Success();
    }

    #endregion




    #region PRIVATE METHODS

    private Result ChangeStatus(Reason reason, IAccountStatusPolicy policy, AccountStatus targetStatus)
    {
        var checkResult = policy.CanTransit(current: CurrentStatus, target: targetStatus);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "AccountId", value: Id));

        CurrentStatus = targetStatus;

        return Result.Success();
    }

    #endregion
}

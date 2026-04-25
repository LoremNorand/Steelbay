using Steelbay.Domain.Accounts.AccountStatuses;
using Steelbay.Domain.Common.Results;
using Steelbay.Domain.Common.ValueObjects;




namespace Steelbay.Domain.Accounts;

public static class AccountErrors
{
    #region PUBLIC STATIC METHODS

    public static Error ActionRestricted(AccountAction action, AccountStatus currentStatus)
    {
        var code = "account:action_restricted";
        var description = $"[{currentStatus}] account cannot execute [{action}]";

        return Error.Create(code: code, description: description);
    }


    public static Error EmailUpdateRedundant(Email email)
    {
        var code = "account:email_update_redundant";
        var description = $"Email [{email}] cannot be replaced with the same";

        return Error.Create(code: code, description: description);
    }


    public static Error StatusAlreadyAssigned(AccountStatus targetStatus)
    {
        var code = "account:status_already_assigned";
        var description = $"The target status [{targetStatus}] has already been assigned to the account";

        return Error.Create(code: code, description: description);
    }


    public static Error StatusTransition(AccountStatus current, AccountStatus target)
    {
        var code = "account:status_transition";
        var description = $"Cannot change account status from [{current}] to [{target}]";

        return Error.Create(code: code, description: description);
    }

    #endregion
}

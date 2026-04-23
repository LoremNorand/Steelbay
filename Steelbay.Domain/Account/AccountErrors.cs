using Steelbay.Domain.Common.Result;
using Steelbay.Domain.Common.ValueObjects;




namespace Steelbay.Domain.Account;

public static class AccountErrors
{
    #region PUBLIC STATIC METHODS

    public static Error CurrentStatusIsNull()
    {
        var code = "account:current_status_is_null";
        var description = "Current account status is null";

        return Error.Create(code, description);
    }


    public static Error EmailUpdateRedundant(Email email)
    {
        var code = "account:email_update_redundant";
        var description = $"Email [{email}] cannot be replaced with the same";

        return Error.Create(code, description);
    }


    public static Error StatusAlreadyAssigned(AccountStatus.AccountStatus targetStatus)
    {
        var code = "account:status_already_assigned";
        var description = $"The target status [{targetStatus}] has already been assigned to the account";

        return Error.Create(code, description);
    }


    public static Error StatusTransition(AccountStatus.AccountStatus current, AccountStatus.AccountStatus target)
    {
        var code = "account:status_transition";
        var description = $"Cannot change account status from [{current}] to [{target}]";

        return Error.Create(code, description);
    }

    #endregion
}

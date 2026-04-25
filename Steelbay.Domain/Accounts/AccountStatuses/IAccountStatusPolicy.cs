using Steelbay.Domain.Common.Results;




namespace Steelbay.Domain.Accounts.AccountStatuses;

public interface IAccountStatusPolicy
{
    #region PUBLIC METHODS

    Result CanExecute(AccountStatus current, AccountAction action);
    Result CanTransit(AccountStatus current, AccountStatus target);

    #endregion
}

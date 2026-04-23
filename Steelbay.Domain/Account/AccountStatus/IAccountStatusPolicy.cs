using Steelbay.Domain.Common.Result;




namespace Steelbay.Domain.Account.AccountStatus;

public interface IAccountStatusPolicy
{
    #region PUBLIC METHODS

    Result CanExecute(AccountStatus current, AccountAction action);
    Result CanTransit(AccountStatus current, AccountStatus target);

    #endregion
}

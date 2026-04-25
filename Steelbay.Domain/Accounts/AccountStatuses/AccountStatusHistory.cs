using Steelbay.Domain.Common.Histories;




namespace Steelbay.Domain.Accounts.AccountStatuses;

public class AccountStatusHistory : IHistory<AccountStatusCommit>
{
    #region READONLY FIELDS

    private readonly List<AccountStatusCommit> _history = new();

    #endregion




    #region CONSTRUCTORS

    protected AccountStatusHistory()
    { }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IHistory<AccountStatusCommit>

    public void Commit(AccountStatusCommit value)
    {
        _history.Add(value);
        Current = value;
    }


    public AccountStatusCommit? Current { get; private set; }
    public IReadOnlyCollection<AccountStatusCommit> History => _history.AsReadOnly();

    #endregion

    #endregion




    #region PUBLIC STATIC METHODS

    public static AccountStatusHistory Create() => new();

    #endregion
}

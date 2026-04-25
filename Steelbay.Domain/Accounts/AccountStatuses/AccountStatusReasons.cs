using Steelbay.Domain.Common.Interfaces;
using Steelbay.Domain.Common.Results;
using Steelbay.Domain.Common.ValueObjects;




namespace Steelbay.Domain.Accounts.AccountStatuses;

public static class AccountStatusReasons
{
    #region PUBLIC STATIC METHODS

    public static Reason DeleteByUser(IAuditMetadata? metadata)
    {
        var code = "Account.Status.DeleteByUser";
        var meta = metadata?.ToMetadata();
        var initiator = ReasonInitiator.User;
        var isImplicit = true;

        return Reason.Create(code, meta, initiator, isImplicit);
    }

    #endregion
}

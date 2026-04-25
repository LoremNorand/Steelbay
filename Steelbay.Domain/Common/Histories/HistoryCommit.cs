using Steelbay.Domain.Common.Interfaces;
using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.Histories;

public abstract class HistoryCommit : ValueObject, IAuditMetadata
{
    #region PUBLIC PROPERTIES

    public DateTime CreatedAtUtc { get; protected set; }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IAuditMetadata

    public abstract object ToMetadata();

    #endregion

    #endregion
}

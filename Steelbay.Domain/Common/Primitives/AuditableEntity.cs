using Steelbay.Domain.Common.Interfaces;




namespace Steelbay.Domain.Common.Primitives;

public abstract class AuditableEntity<TId> : Entity<TId>, IAuditable where TId : notnull
{
    #region CONSTRUCTORS

    protected AuditableEntity(TId id) : base(id)
    { }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IAuditable

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }

    #endregion

    #endregion
}

namespace Steelbay.Domain.Common.Interfaces;

public interface IAuditable
{
    #region PUBLIC PROPERTIES

    public DateTime CreatedOnUtc { get; protected set; }
    public DateTime? UpdatedOnUtc { get; protected set; }

    #endregion
}

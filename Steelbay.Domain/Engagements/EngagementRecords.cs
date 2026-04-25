using Steelbay.Domain.Common.Utils;




namespace Steelbay.Domain.Engagements;

public record EngagementId(Guid Value)
{
    #region PUBLIC STATIC METHODS

    public static EngagementId Generate() => new(IdGenerator.NewId());

    #endregion
}

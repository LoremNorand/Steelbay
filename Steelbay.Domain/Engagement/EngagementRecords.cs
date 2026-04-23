using Steelbay.Domain.Common.Util;




namespace Steelbay.Domain.Engagement;

public record EngagementId(Guid Value)
{
    #region PUBLIC STATIC METHODS

    public static EngagementId Generate() => new(IdGenerator.NewId());

    #endregion
}

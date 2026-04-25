using Steelbay.Domain.Common.Results;




namespace Steelbay.Domain.Engagements;

public interface IEngagementPolicy
{
    #region PUBLIC METHODS

    public Result CanExecute(Engagement engagement, EngagementAction action);

    #endregion
}

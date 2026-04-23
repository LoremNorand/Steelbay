using Steelbay.Domain.Common.Result;




namespace Steelbay.Domain.Engagement;

public interface IEngagementPolicy
{
    #region PUBLIC METHODS

    public Result CanExecute(Engagement engagement, EngagementAction action);

    #endregion
}

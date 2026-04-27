using Steelbay.Domain.Common.Results;
using Steelbay.Domain.Common.ValueObjects.Documents;




namespace Steelbay.Domain.Engagements;

public static class EngagementErrors
{
    #region PUBLIC STATIC METHODS

    public static Error ActionRestricted(EngagementAction action)
    {
        var code = "engagement:action_restricted";
        var description = $"Engagement cannot execute [{action}]";

        return Error.Create(code: code, description: description);
    }


    public static Error IncorrectFileType(Key key)
    {
        var code = "engagement:incorrect_file_type";
        var description = $"[{key.KeyType}] from [{key}] is incorrect file type for engagement gallery";

        return Error.Create(code: code, description: description);
    }


    public static Error IndexOutOfBound(int index, int length)
    {
        var code = "engagement:index_out_of_bound";
        var description = "given index [{index}] is out of bound of gallery length [{length}]";

        return Error.Create(code: code, description: description);
    }

    #endregion
}

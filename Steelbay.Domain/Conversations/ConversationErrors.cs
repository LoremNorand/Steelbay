using Steelbay.Domain.Accounts;
using Steelbay.Domain.Common.Results;




namespace Steelbay.Domain.Conversations;

public static class ConversationErrors
{
    #region PUBLIC STATIC METHODS

    public static Error ParticipantAlreadyExists(AccountId accountId)
    {
        var code = "conversation:participant_already_exists";
        var description = $"Participant with account [{accountId}] is already in the conversation";

        return Error.Create(code: code, description: description);
    }


    public static Error ParticipantNotFound(AccountId accountId)
    {
        var code = "conversation:participant_not_found";
        var description = $"Participant with account [{accountId}] was not found in the conversation";

        return Error.Create(code: code, description: description);
    }


    public static Error ActionRestricted(ConversationAction action)
    {
        var code = "conversation:action_restricted";
        var description = $"Action [{action}] is restricted by policy";

        return Error.Create(code: code, description: description);
    }


    public static Error AlreadyArchived()
    {
        var code = "conversation:already_archived";
        var description = "Conversation is already archived and cannot be modified";

        return Error.Create(code: code, description: description);
    }

    #endregion
}

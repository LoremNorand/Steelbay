using Steelbay.Domain.Common.Results;




namespace Steelbay.Domain.Conversations;

public interface IConversationPolicy
{
    Result CanExecute(Conversation conversation, ConversationAction action, Guid actorId);
}

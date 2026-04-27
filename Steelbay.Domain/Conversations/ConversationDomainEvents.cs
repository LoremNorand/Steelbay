using Steelbay.Domain.Accounts;
using Steelbay.Domain.Common.DomainEvents;




namespace Steelbay.Domain.Conversations;

public record ConversationCreated(ConversationId ConversationId, ReferenceId ReferenceId) : DomainEvent;

public record ConversationArchived(ConversationId ConversationId) : DomainEvent;

public record MessageSent(ConversationId ConversationId, Message Message) : DomainEvent;

public record ParticipantInvited(ConversationId ConversationId, Participant Participant) : DomainEvent;

public record ParticipantRemoved(ConversationId ConversationId, AccountId AccountId) : DomainEvent;

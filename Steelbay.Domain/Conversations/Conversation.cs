using Steelbay.Domain.Accounts;
using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Results;




namespace Steelbay.Domain.Conversations;

public class Conversation : AggregateRoot<ConversationId>
{
    #region READONLY FIELDS

    private readonly List<Message> _messages = new();
    private readonly List<Participant> _participants = new();

    #endregion




    #region PUBLIC PROPERTIES

    public bool IsArchived { get; private set; }
    public ReferenceId ReferenceId { get; private set; }

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();
    public IReadOnlyCollection<Participant> Participants => _participants.AsReadOnly();

    #endregion




    #region CONSTRUCTORS

#pragma warning disable CS8618
    // EF CORE Private constructor
    private Conversation() : base(id: null!)
    { }
#pragma warning restore CS8618


    private Conversation(ConversationId id, ReferenceId referenceId) : base(id: id)
    {
        ReferenceId = referenceId;
        IsArchived = false;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Conversation Create(ReferenceId referenceId)
    {
        var id = ConversationId.Generate();
        var conversation = new Conversation(id: id, referenceId: referenceId);

        conversation.AddDomainEvent(domainEvent: new ConversationCreated(ConversationId: conversation.Id, ReferenceId: referenceId));

        return conversation;
    }

    #endregion




    #region PUBLIC METHODS

    public Result InviteParticipant(Participant participant, AccountId actorId, IConversationPolicy policy)
    {
        if (IsArchived)
            return Result.Failure(error: ConversationErrors.AlreadyArchived());

        var checkResult = policy.CanExecute(conversation: this, action: ConversationAction.InviteParticipant, actorId: actorId.Value);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "ConversationId", value: Id));

        if (_participants.Any(predicate: p => p.AccountId == participant.AccountId))
            return Result.Failure(error: ConversationErrors.ParticipantAlreadyExists(accountId: participant.AccountId)
                                                           .WithContext(key: "ConversationId", value: Id));

        _participants.Add(item: participant);
        AddDomainEvent(domainEvent: new ParticipantInvited(ConversationId: Id, Participant: participant));

        return Result.Success();
    }


    public Result RemoveParticipant(AccountId accountId, AccountId actorId, IConversationPolicy policy)
    {
        if (IsArchived)
            return Result.Failure(error: ConversationErrors.AlreadyArchived());

        var checkResult = policy.CanExecute(conversation: this, action: ConversationAction.RemoveParticipant, actorId: actorId.Value);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "ConversationId", value: Id));

        var participant = _participants.FirstOrDefault(predicate: p => p.AccountId == accountId);

        if (participant is null)
            return Result.Failure(error: ConversationErrors.ParticipantNotFound(accountId: accountId)
                                                           .WithContext(key: "ConversationId", value: Id));

        _participants.Remove(item: participant);
        AddDomainEvent(domainEvent: new ParticipantRemoved(ConversationId: Id, AccountId: accountId));

        return Result.Success();
    }


    public Result SendMessage(Message message, IConversationPolicy policy)
    {
        if (IsArchived)
            return Result.Failure(error: ConversationErrors.AlreadyArchived());

        var checkResult = policy.CanExecute(conversation: this, action: ConversationAction.SendMessage, actorId: message.AuthorId.Value);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "ConversationId", value: Id));

        _messages.Add(item: message);
        AddDomainEvent(domainEvent: new MessageSent(ConversationId: Id, Message: message));

        return Result.Success();
    }


    public Result Archive(AccountId actorId, IConversationPolicy policy)
    {
        if (IsArchived)
            return Result.Success();

        var checkResult = policy.CanExecute(conversation: this, action: ConversationAction.Archive, actorId: actorId.Value);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "ConversationId", value: Id));

        IsArchived = true;
        AddDomainEvent(domainEvent: new ConversationArchived(ConversationId: Id));

        return Result.Success();
    }

    #endregion
}

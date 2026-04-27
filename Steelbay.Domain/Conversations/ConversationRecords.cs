using Steelbay.Domain.Common.Utils;




namespace Steelbay.Domain.Conversations;

public record ConversationId(Guid Value)
{
    #region PUBLIC STATIC METHODS

    public static ConversationId Generate() => new(Value: IdGenerator.NewId());

    #endregion
}


public record ReferenceId(Guid Value);

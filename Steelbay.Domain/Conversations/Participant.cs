using Steelbay.Domain.Accounts;
using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Conversations;

public sealed class Participant : ValueObject
{
    #region PUBLIC PROPERTIES

    public AccountId AccountId { get; }
    public ParticipantRole Role { get; }

    #endregion




    #region CONSTRUCTORS

    private Participant(AccountId accountId, ParticipantRole role)
    {
        AccountId = accountId;
        Role = role;
    }


#pragma warning disable CS8618
    // EF CORE Private constructor
    private Participant()
    { }
#pragma warning restore CS8618

    #endregion




    #region PUBLIC STATIC METHODS

    public static Participant Create(AccountId accountId, ParticipantRole role)
    {
        if (accountId is null)
            throw new ArgumentNullException(paramName: nameof(accountId));

        if (!Enum.IsDefined(value: role))
            throw new ArgumentOutOfRangeException(paramName: nameof(role), actualValue: role, message: "Unknown participant role.");

        var participant = new Participant(accountId: accountId, role: role);

        return participant;
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return AccountId;
        yield return Role;
    }

    #endregion
}

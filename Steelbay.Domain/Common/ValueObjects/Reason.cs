using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Results;
using Steelbay.Domain.Common.Utils;




namespace Steelbay.Domain.Common.ValueObjects;

public class Reason : ValueObject
{
    #region PUBLIC PROPERTIES

    public string Code { get; }

    public Guid Id { get; }
    public ReasonInitiator Initiator { get; }

    public bool IsImplicit { get; }
    public object? Metadata { get; }

    #endregion




    #region CONSTRUCTORS

    private Reason(Guid id, string code, object? metadata, ReasonInitiator initiator, bool isImplicit)
    {
        Code = code;
        Metadata = metadata;
        Initiator = initiator;
        Id = id;
        IsImplicit = isImplicit;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Reason Create(string code, object? metadata, ReasonInitiator initiator, bool isImplicit)
    {
        var preCode = code;
        var preInitiator = initiator;

        var id = IdGenerator.NewId();

        return new Reason(id: id, code: preCode, metadata: metadata, initiator: preInitiator, isImplicit: isImplicit);
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    #endregion
}

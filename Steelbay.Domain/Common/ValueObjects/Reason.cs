using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Results;
using System.Text.RegularExpressions;




namespace Steelbay.Domain.Common.ValueObjects;

public sealed class Reason : ValueObject
{
    #region READONLY FIELDS

    private static readonly Regex _codeRegex = new(
        pattern: @"^[a-z][a-z0-9_]*:[a-z][a-z0-9_]*$",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase);

    #endregion




    #region PUBLIC PROPERTIES

    public string Code { get; }
    public ReasonInitiator Initiator { get; }

    public bool IsImplicit { get; }
    public object? Metadata { get; }

    #endregion




    #region CONSTRUCTORS

    private Reason()
    {
        Code = null!;
    }


    private Reason(string code, object? metadata, ReasonInitiator initiator, bool isImplicit)
    {
        Code = code;
        Metadata = metadata;
        Initiator = initiator;
        IsImplicit = isImplicit;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Reason Create(string code, object? metadata, ReasonInitiator initiator, bool isImplicit)
    {
        if (string.IsNullOrWhiteSpace(value: code))
            throw new ArgumentException(message: "Reason code cannot be empty.", paramName: nameof(code));

        var normalizedCode = code.Trim();

        if (!_codeRegex.IsMatch(input: normalizedCode))
            throw new ArgumentException(message: $"Reason code must match 'header:specific_code_name'. Actual: {code}",
                paramName: nameof(code));

        if (!Enum.IsDefined(value: initiator))
            throw new ArgumentOutOfRangeException(paramName: nameof(initiator), actualValue: initiator,
                message: "Unknown reason initiator.");

        return new Reason(code: normalizedCode, metadata: metadata, initiator: initiator, isImplicit: isImplicit);
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Code;
        yield return Initiator;
        yield return IsImplicit;
        yield return Metadata;
    }

    #endregion
}

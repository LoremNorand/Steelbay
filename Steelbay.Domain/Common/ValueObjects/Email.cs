using System.Text.RegularExpressions;
using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.ValueObjects;

public sealed class Email : ValueObject
{
    #region READONLY FIELDS

    private static readonly Regex _emailRegex = new(
        pattern: @"^[^@\s]+@[^@\s]+\.[^@\s]+$", options: RegexOptions.Compiled | RegexOptions.IgnoreCase);

    #endregion




    #region PUBLIC PROPERTIES

    public string Value { get; }

    #endregion




    #region OPERATORS

    public static implicit operator string(Email email) => email.Value;

    #endregion




    #region CONSTRUCTORS

    private Email(string value)
    {
        Value = value;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Email Create(string value)
    {
        if (string.IsNullOrEmpty(value: value))
            throw new ArgumentNullException(paramName: nameof(value));

        var trimmed = value.Trim().ToLower();

        if (!_emailRegex.IsMatch(input: trimmed))
            throw new ArgumentException(message: $"Invalid email: {value}");

        return new Email(value: trimmed);
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    #endregion
}

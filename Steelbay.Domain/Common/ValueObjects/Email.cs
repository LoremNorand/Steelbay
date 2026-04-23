using System.Text.RegularExpressions;
using Steelbay.Domain.Common.Primitives;




namespace Steelbay.Domain.Common.ValueObjects;

public sealed class Email : ValueObject
{
    #region READONLY FIELDS

    private static readonly Regex _emailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException(nameof(value));

        var trimmed = value.Trim().ToLower();

        if (!_emailRegex.IsMatch(trimmed))
            throw new ArgumentException($"Invalid email: {value}");

        return new Email(trimmed);
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    #endregion
}

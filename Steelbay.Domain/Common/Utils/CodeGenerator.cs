namespace Steelbay.Domain.Common.Utils;

public static class CodeGenerator
{
    #region CONSTANTS

    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    #endregion




    #region READONLY FIELDS

    private static Func<string> _generationMethod = DefaultGenerationMethod;
    private static readonly Random _random = new();

    #endregion




    #region STATIC FIELDS

    #endregion




    #region PUBLIC STATIC METHODS

    public static string Generate() => _generationMethod();


    public static void ResetGenerationMethod() => _generationMethod = DefaultGenerationMethod;


    public static void SetGenerationMethod(Func<string> method) => _generationMethod = method;

    #endregion




    #region PRIVATE METHODS

    private static string DefaultGenerationMethod()
    {
        var now = DateTime.UtcNow;

        // YY (2) + '-' (1) + MM (2) + '-' (1) + XXXX (4) = 10 символов
        return string.Create(length: 10, state: now, action: (span, date) =>
        {
            var year = date.Year % 100;
            span[index: 0] = (char)('0' + year / 10);
            span[index: 1] = (char)('0' + year % 10);
            span[index: 2] = '-';
            var month = date.Month;
            span[index: 3] = (char)('0' + month / 10);
            span[index: 4] = (char)('0' + month % 10);
            span[index: 5] = '-';

            for (var i = 6; i < 10; i++) span[index: i] = Alphabet[index: _random.Next(maxValue: Alphabet.Length)];
        });
    }

    #endregion
}

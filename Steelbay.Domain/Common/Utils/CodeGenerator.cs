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
        return string.Create(10, now, (span, date) =>
        {
            var year = date.Year % 100;
            span[0] = (char)('0' + year / 10);
            span[1] = (char)('0' + year % 10);
            span[2] = '-';
            var month = date.Month;
            span[3] = (char)('0' + month / 10);
            span[4] = (char)('0' + month % 10);
            span[5] = '-';

            for (var i = 6; i < 10; i++) span[i] = Alphabet[_random.Next(Alphabet.Length)];
        });
    }

    #endregion
}

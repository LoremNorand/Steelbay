namespace Steelbay.Domain.Common.Utils;

internal static class IdGenerator
{
    #region READONLY FIELDS

    private static readonly Func<Guid> _defaultGenerationMethod = Guid.CreateVersion7;

    #endregion




    #region STATIC FIELDS

    private static Func<Guid> _generationMethod = () => _defaultGenerationMethod();

    #endregion




    #region PUBLIC STATIC METHODS

    public static Guid NewId() => _generationMethod();

    public static void ResetGenerationMethod() => _generationMethod = _defaultGenerationMethod;

    public static void SetGenerationMethod(Func<Guid> generationMethod) => _generationMethod = generationMethod;

    #endregion
}

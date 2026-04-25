using System.Collections.Frozen;




namespace Steelbay.Domain.Common.ValueObjects.Documents;

public static class ExtensionWhitelist
{
    #region PUBLIC PROPERTIES

    public static FrozenSet<string> ForDocument { get; } = new[]
    {
        ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt", ".rtf", ".md"
    }.ToFrozenSet(comparer: StringComparer.OrdinalIgnoreCase);

    public static FrozenSet<string> ForImage { get; } = new[]
    {
        ".jpg", ".jpeg", ".png", ".webp", ".avif", ".heic", ".heif", ".gif", ".bmp", ".tiff"
    }.ToFrozenSet(comparer: StringComparer.OrdinalIgnoreCase);

    public static FrozenSet<string> ForVideo { get; } = new[]
    {
        ".mp4", ".webm", ".mov", ".mpeg", ".mpg"
    }.ToFrozenSet(comparer: StringComparer.OrdinalIgnoreCase);

    #endregion
}

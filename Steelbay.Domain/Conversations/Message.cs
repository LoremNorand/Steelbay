using Steelbay.Domain.Accounts;
using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.ValueObjects.Documents;




namespace Steelbay.Domain.Conversations;

public class Message : ValueObject
{
    #region READONLY FIELDS

    private readonly List<Key> _files;

    #endregion




    #region PUBLIC PROPERTIES

    public AccountId AuthorId { get; }
    public string Content { get; }
    public DateTime CreatedAtUtc { get; }
    public bool IsEdited { get; }
    public IReadOnlyCollection<Key> Files => _files.AsReadOnly();

    #endregion




    #region CONSTRUCTORS

#pragma warning disable CS8618
    // EF CORE Private constructor
    private Message()
    { }
#pragma warning restore CS8618


    private Message(AccountId authorId, string content, List<Key>? files, DateTime createdAt, bool isEdited)
    {
        AuthorId = authorId;
        Content = content;
        _files = files ?? new List<Key>();
        CreatedAtUtc = createdAt;
        IsEdited = isEdited;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Message Create(AccountId authorId, string content, IReadOnlyCollection<Key>? files = null)
    {
        EnsureValid(authorId: authorId, content: content, files: files);

        var now = DateTime.UtcNow;
        var normalizedFiles = files?.ToList() ?? new List<Key>();
        var normalizedContent = NormalizeContent(content: content);
        var message = new Message(authorId: authorId, content: normalizedContent, files: normalizedFiles, createdAt: now, isEdited: false);

        return message;
    }

    #endregion




    #region PUBLIC METHODS

    public Message Edit(string content, IReadOnlyCollection<Key>? files = null)
    {
        EnsureValid(authorId: AuthorId, content: content, files: files);

        var normalizedFiles = files?.ToList() ?? new List<Key>();
        var normalizedContent = NormalizeContent(content: content);

        var message = new Message(authorId: AuthorId, content: normalizedContent, files: normalizedFiles, createdAt: CreatedAtUtc,
            isEdited: true);

        return message;
    }

    #endregion




    #region PROTECTED METHODS

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Content;
        yield return AuthorId;
        yield return CreatedAtUtc;
        yield return IsEdited;

        foreach (var file in _files)
            yield return file;
    }

    #endregion




    #region PRIVATE METHODS

    private static void EnsureValid(AccountId authorId, string content, IReadOnlyCollection<Key>? files)
    {
        if (authorId is null)
            throw new ArgumentNullException(paramName: nameof(authorId));

        if (string.IsNullOrWhiteSpace(value: content) && (files is null || files.Count == 0))
            throw new ArgumentException(message: "Message must contain text or at least one file.");

        if (files?.Any(predicate: file => file is null) == true)
            throw new ArgumentException(message: "Message files cannot contain null values.", paramName: nameof(files));
    }


    private static string NormalizeContent(string content) => string.IsNullOrWhiteSpace(value: content) ? string.Empty : content;

    #endregion
}

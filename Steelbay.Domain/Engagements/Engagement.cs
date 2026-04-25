using Steelbay.Domain.Common.Primitives;
using Steelbay.Domain.Common.Results;
using Steelbay.Domain.Common.ValueObjects;
using Steelbay.Domain.Common.ValueObjects.Documents;




namespace Steelbay.Domain.Engagements;

public class Engagement : AggregateRoot<EngagementId>
{
    #region READONLY FIELDS

    private readonly List<Key> _gallery;

    #endregion




    #region PUBLIC PROPERTIES

    public string Description { get; private set; }
    public string Name { get; private set; }
    public Price Price { get; private set; }
    public int Views { get; private set; }

    public IReadOnlyCollection<Key> Gallery => _gallery.AsReadOnly();

    #endregion




    #region CONSTRUCTORS

    private Engagement(EngagementId id, string name, string description, Price price, List<Key> gallery) : base(id: id)
    {
        Name = name;
        Description = description;
        Price = price;
        _gallery = gallery;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Engagement Create(string name, string description, Price price, List<Key>? gallery = null)
    {
        var id = EngagementId.Generate();
        var g = gallery ?? new List<Key>();
        var engagement = new Engagement(id: id, name: name, description: description, price: price, gallery: g);

        engagement.AddDomainEvent(domainEvent: new EngagementCreated(EngagementId: engagement.Id));

        return engagement;
    }

    #endregion




    #region PUBLIC METHODS

    public Result AppendGallery(Key key, IEngagementPolicy policy)
    {
        if (key.KeyType != KeyType.Image && key.KeyType != KeyType.Video)
            return Result.Failure(
                error: Error.Create(
                    code: "engagement:incorrect_file_type",
                    description: $"{key.KeyType} is incorrect file type")
            );

        var checkResult = policy.CanExecute(engagement: this, action: EngagementAction.AppendGallery);

        _gallery.Add(item: key);

        return checkResult;
    }


    public Result ReduceGallery(int index, IEngagementPolicy policy)
    {
        var checkResult = policy.CanExecute(engagement: this, action: EngagementAction.ReduceGallery);

        _gallery.RemoveAt(index: index);

        return checkResult;
    }


    public Result ReorderGallery(int oldIndex, int newIndex, IEngagementPolicy policy)
    {
        var checkResult = policy.CanExecute(engagement: this, action: EngagementAction.ReorderGallery);

        if (oldIndex < 0 || oldIndex >= _gallery.Count ||
            newIndex < 0 || newIndex >= _gallery.Count ||
            oldIndex == newIndex)
            return Result.Failure(error: Error.Create(code: "engagement:gallery_out_of_bound", description: ""));

        var item = _gallery[index: oldIndex];
        _gallery.RemoveAt(index: oldIndex);
        _gallery.Insert(index: newIndex, item: item);

        return checkResult;
    }

    #endregion
}

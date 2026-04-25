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

    public IReadOnlyCollection<Key> Gallery => _gallery.AsReadOnly();

    #endregion




    #region CONSTRUCTORS

    private Engagement(EngagementId id, string name, string description, Price price, List<Key> gallery) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        _gallery = gallery;
    }

    #endregion




    #region PUBLIC STATIC METHODS

    public static Engagement Create(string name, string description, Price price, List<Key> gallery)
    {
        var id = EngagementId.Generate();

        return new Engagement(id, name, description, price, gallery);
    }

    #endregion




    #region PUBLIC METHODS

    public Result AppendGallery(Key key, IEngagementPolicy policy)
    {
        if (key.KeyType != KeyType.Image && key.KeyType != KeyType.Video)
            return Result.Failure(
                Error.Create(
                    "engagement:incorrect_file_type",
                    $"{key.KeyType} is incorrect file type")
            );

        var checkResult = policy.CanExecute(this, EngagementAction.AppendGallery);

        _gallery.Add(key);

        return checkResult;
    }


    public Result ReduceGallery(int index, IEngagementPolicy policy)
    {
        var checkResult = policy.CanExecute(this, EngagementAction.ReduceGallery);

        _gallery.RemoveAt(index);

        return checkResult;
    }


    public Result ReorderGallery(int oldIndex, int newIndex, IEngagementPolicy policy)
    {
        var checkResult = policy.CanExecute(this, EngagementAction.ReorderGallery);

        if (oldIndex < 0 || oldIndex >= _gallery.Count ||
            newIndex < 0 || newIndex >= _gallery.Count ||
            oldIndex == newIndex)
            return Result.Failure(Error.Create("engagement:gallery_out_of_bound", ""));

        var item = _gallery[oldIndex];
        _gallery.RemoveAt(oldIndex);
        _gallery.Insert(newIndex, item);

        return checkResult;
    }

    #endregion
}

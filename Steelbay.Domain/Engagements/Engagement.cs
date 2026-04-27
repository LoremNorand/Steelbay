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
    public bool IsVisible { get; private set; }
    public string Name { get; private set; }
    public Price Price { get; private set; }

    public IReadOnlyCollection<Key> Gallery => _gallery.AsReadOnly();

    #endregion




    #region CONSTRUCTORS

#pragma warning disable CS8618
    // EF CORE Private constructor
    private Engagement() : base(id: null!)
    { }
#pragma warning restore CS8618


    private Engagement(EngagementId id, string name, string description, Price price, List<Key> gallery) : base(id: id)
    {
        Name = name;
        Description = description;
        Price = price;
        _gallery = gallery;
        IsVisible = true;
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
            return Result.Failure(error: EngagementErrors.IncorrectFileType(key: key).WithContext(key: "EngagementId", value: Id));

        var checkResult = policy.CanExecute(engagement: this, action: EngagementAction.AppendGallery);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "EngagementId", value: Id));

        _gallery.Add(item: key);
        AddDomainEvent(domainEvent: new EngagementGalleryAppended(EngagementId: Id, Key: key));

        return Result.Success();
    }


    public Result ReduceGallery(int index, IEngagementPolicy policy)
    {
        var checkIndex = CheckIndex(index: index);

        if (checkIndex.IsFailure)
            return Result.Failure(error: checkIndex.Error.WithContext(key: "EngagementId", value: Id));

        var checkResult = policy.CanExecute(engagement: this, action: EngagementAction.ReduceGallery);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "EngagementId", value: Id));

        _gallery.RemoveAt(index: index);
        AddDomainEvent(domainEvent: new EngagementGalleryReduced(EngagementId: Id, DeletionIndex: index));

        return Result.Success();
    }


    public Result ReorderGallery(int oldIndex, int newIndex, IEngagementPolicy policy)
    {
        if (oldIndex == newIndex) return Result.Success();

        var checkOld = CheckIndex(index: oldIndex);
        var checkNew = CheckIndex(index: newIndex);

        if (checkOld.IsFailure) return Result.Failure(error: checkOld.Error.WithContext(key: "EngagementId", value: Id));
        if (checkNew.IsFailure) return Result.Failure(error: checkNew.Error.WithContext(key: "EngagementId", value: Id));

        var checkResult = policy.CanExecute(engagement: this, action: EngagementAction.ReorderGallery);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "EngagementId", value: Id));

        var item = _gallery[index: oldIndex];
        _gallery.RemoveAt(index: oldIndex);
        _gallery.Insert(index: newIndex, item: item);

        AddDomainEvent(domainEvent: new EngagementGalleryReordered(EngagementId: Id, OldIndex: oldIndex, NewIndex: newIndex));

        return Result.Success();
    }


    public Result UpdateDescription(string description, IEngagementPolicy policy)
    {
        if (Description == description) return Result.Success();

        var checkResult = policy.CanExecute(engagement: this, action: EngagementAction.UpdateDescription);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "EngagementId", value: Id));

        var oldDescription = Description;
        Description = description;

        AddDomainEvent(domainEvent: new EngagementDescriptionUpdated(EngagementId: Id, OldDescription: oldDescription,
            NewDescription: description));

        return Result.Success();
    }


    public Result UpdateName(string name, IEngagementPolicy policy)
    {
        if (Name == name) return Result.Success();

        var checkResult = policy.CanExecute(engagement: this, action: EngagementAction.UpdateName);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "EngagementId", value: Id));

        var oldName = Name;
        Name = name;

        AddDomainEvent(domainEvent: new EngagementNameUpdated(EngagementId: Id, OldName: oldName, NewName: name));

        return Result.Success();
    }


    public Result UpdatePrice(Price price, IEngagementPolicy policy)
    {
        if (Price.Equals(other: price)) return Result.Success();

        var checkResult = policy.CanExecute(engagement: this, action: EngagementAction.UpdatePrice);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "EngagementId", value: Id));

        var oldPrice = Price;
        Price = price;

        AddDomainEvent(domainEvent: new EngagementPriceUpdated(EngagementId: Id, OldPrice: oldPrice, NewPrice: price));

        return Result.Success();
    }


    public Result UpdateVisibility(IEngagementPolicy policy)
    {
        var checkResult = policy.CanExecute(engagement: this, action: EngagementAction.UpdateVisibility);

        if (checkResult.IsFailure)
            return Result.Failure(error: checkResult.Error.WithContext(key: "EngagementId", value: Id));

        IsVisible = !IsVisible;
        AddDomainEvent(domainEvent: new EngagementVisibilityToggled(EngagementId: Id, IsVisible: IsVisible));

        return Result.Success();
    }

    #endregion




    #region PRIVATE METHODS

    private Result CheckIndex(int index)
    {
        if (index < 0 || index >= _gallery.Count)
            return Result.Failure(error: EngagementErrors.IndexOutOfBound(index: index, length: _gallery.Count));

        return Result.Success();
    }

    #endregion
}

namespace Steelbay.Domain.Common.Primitives;

public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    #region PUBLIC PROPERTIES

    public TId Id { get; protected set; }

    #endregion




    #region OPERATORS

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(objA: left, objB: right);

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);

    #endregion




    #region CONSTRUCTORS

    protected Entity(TId id)
    {
        Id = id;
    }

    #endregion




    #region INTERFACE IMPLEMENTATIONS

    #region IEquatable<Entity<TId>>

    public bool Equals(Entity<TId>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(objA: this, objB: other)) return true;

        return Id.Equals(obj: other.Id);
    }

    #endregion

    #endregion




    #region PUBLIC METHODS

    public override bool Equals(object? obj) => obj is Entity<TId> other && Equals(other: other);

    public override int GetHashCode() => Id.GetHashCode();

    #endregion
}

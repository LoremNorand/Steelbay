namespace Steelbay.Domain.Common.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    #region INTERFACE IMPLEMENTATIONS

    #region IEquatable<ValueObject>

    public bool Equals(ValueObject? other) => Equals(obj: (object)other!);

    #endregion

    #endregion




    #region PUBLIC METHODS

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(second: valueObject.GetEqualityComponents());
    }


    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        foreach (var valueObject in GetEqualityComponents())
            hashCode.Add(value: valueObject.GetHashCode());

        return hashCode.ToHashCode();
    }

    #endregion




    #region PROTECTED METHODS

    protected abstract IEnumerable<object> GetEqualityComponents();

    #endregion
}

namespace Steelbay.Domain.Common.History;

public interface IHistory<T> where T : HistoryCommit
{
    #region PUBLIC PROPERTIES

    public T? Current { get; }
    public IReadOnlyCollection<T> History { get; }

    #endregion




    #region PUBLIC METHODS

    public void Commit(T value);

    #endregion
}

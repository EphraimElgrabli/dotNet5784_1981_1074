namespace BlApi;

public interface IBl
{
    public IUser User { get; }
    public ITask Task { get; }
    public IClock Clock { get; }
}

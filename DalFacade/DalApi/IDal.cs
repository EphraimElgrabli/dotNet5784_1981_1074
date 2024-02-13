namespace DalApi;


public interface IDal
{
    IDependency Dependency { get; }
    ITask Task { get; }
    IUser User { get; }
    IClock Clock { get; }
}
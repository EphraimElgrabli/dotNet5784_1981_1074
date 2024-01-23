namespace Dal;
using DalApi;
sealed internal class DalList : IDal

{
    private DalList() { }
    public static IDal Instance { get; } = new DalList();//stage 1.5 we need to do
    public IDependency Dependency => new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();

    public IUser User =>  new UserImplementation();

    
}

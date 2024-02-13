using System.Diagnostics;
using DalApi;
namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IDependency Dependency => new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();

    public IUser User =>  new UserImplementation();
    public IClock Clock => new ClockImplementation();
}

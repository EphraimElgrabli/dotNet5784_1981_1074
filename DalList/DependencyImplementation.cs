namespace Dal;

using System.Collections.Immutable;
using DalApi;
using DO;

public class DependencyImplementation : IDependency
{

    public int Create(Dependency item)
    {
        int id= DataSource.Config.NextDependencyId;
        Dependency dependency = new Dependency();
        dependency = item with { Id = id };
        DataSource.Dependencys.Add(dependency);
        return id; 
    }

    public void Delete(int id)
    {
        Dependency? Dependency = Read(id);
        if ( Dependency == null)
            throw new Exception($"Dependency with ID={id} not exists");
        DataSource.Dependencys.Remove(Dependency);///i search Dependency with id and return the task

    }

    public Dependency? Read(int id)
    {
        Dependency? Dependency = DataSource.Dependencys.Find(D => D.Id == id);///i search for id in the Dependency list
        if (Dependency == null)
        {
            throw new Exception($"Dependency with ID={id} not exists");
        }
        return Dependency;
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

    public void Update(Dependency item)
    {

        if (Read(item.Id) == null)
            throw new Exception($"Dependency with ID={item.Id} not exists");
     
        DataSource.Dependencys.Remove(Read(item.Id)!);/// create new List Dependency with the value of the orginal list and return the new list
        DataSource.Dependencys.Add(item);

    }
}

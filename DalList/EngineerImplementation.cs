namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (Read(item.Id) != null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);///the func add item to the list
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer? Engineer = Read(id);
        if (Engineer == null)
            throw new Exception($"Engineer with ID={id} not exists");
        DataSource.Engineers.Remove(Engineer);///i search Engineer with id and return the task
    }

    public Engineer? Read(int id)
    {
        Engineer? Engineer = DataSource.Engineers.Find(D => D.Id == id);///i search for id in the Engineer list
        if (Engineer == null)
        {
            throw new Exception($"Engineer with ID={id} not exists");
        }
        return Engineer;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        if (Read(item.Id) == null)
            throw new Exception($"Engineer with ID={item.Id} not exists");

        DataSource.Engineers.Remove(Read(item.Id)!);/// create new List Engineer with the value of the orginal list and return the new list
        DataSource.Engineers.Add(item);
    }
}

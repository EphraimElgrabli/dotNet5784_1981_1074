namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        if (Read(item.Id) != null)
            throw new Exception($"Task with ID={item.Id} already exists");
        DataSource.Tasks.Add(item);///the func add item to the list
        return item.Id;
    }

    public void Delete(int id)
    {
        Task? Task = Read(id);///i search Task with id and return the task
        if (Task == null)
            throw new Exception($"Task with ID={id} not exists");
        DataSource.Tasks.Remove(Task);
    }

    public Task? Read(int id)
    {
        Task? Task = DataSource.Tasks.Find(D => D.Id == id);///i search for id in the Task list
        if (Task == null)
        {
            throw new Exception($"Task with ID={id} not exists");
        }
        return Task;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);/// create new List task with the value of the orginal list and return the new list
    }

    public void Update(Task item)
    {
        if (Read(item.Id) == null)
            throw new Exception($"Task with ID={item.Id} not exists");

        DataSource.Tasks.Remove(Read(item.Id)!);/// delete the Task and create new one update
        DataSource.Tasks.Add(item);
    }
}

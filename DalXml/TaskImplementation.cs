namespace Dal;

using System;
using System.Collections.Generic;
using System.Data.Common;
using DalApi;
using DO;

internal class TaskImplementation: ITask
{
    readonly string s_tasks_xml = "tasks";
    /// <summary>
    /// Creates a new task record in the data source.
    /// </summary>
    /// <param name="item">The task to be created.</param>
    /// <returns>The unique identifier of the created task.</returns>
    public int Create(Task item)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        int id = Config.NextTaskId;
        Task task = new Task();
        task = item with { Id = id };
        tasks.Add(task);
        XMLTools.SaveListToXMLSerializer<Task>(tasks,s_tasks_xml);
        return item.Id;
    }

    /// <summary>
    /// Deletes a task record from the data source.
    /// </summary>
    /// <param name="id">The unique identifier of the task to be deleted.</param>
    public void Delete(int id)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        Task? task = Read(id); // Search for the task with the specified ID
        if (task == null)
            throw new DalDoesNotExistException($"Task with ID={id} does not exist");
        tasks.Remove(task); // Remove the task from the data source
        XMLTools.SaveListToXMLSerializer<Task>(tasks, s_tasks_xml);
    }

    /// <summary>
    /// Reads a specific task record from the data source.
    /// </summary>
    /// <param name="id">The unique identifier of the task to be retrieved.</param>
    /// <returns>The task with the specified ID, or null if not found.</returns>
    public Task? Read(int id)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        var query = tasks.FirstOrDefault(u => u.Id == id);
        return query;
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        return (tasks.FirstOrDefault(filter));
    }

    /// <summary>
    /// Retrieves all task records from the data source.
    /// </summary>
    /// <returns>A list containing all task records.</returns>
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        if (filter != null)
        {
            return from task in tasks
                   where filter(task)
                   select task;
        }
        return from task in tasks
               select task;
    }

    /// <summary>
    /// Updates an existing task record in the data source.
    /// </summary>
    /// <param name="item">The updated task information.</param>
    public void Update(Task item)
    {

        if (Read(item.Id) == null)
            throw new DalDoesNotExistException($"Task with ID={item.Id} does not exist");
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        tasks.Remove(Read(item.Id)!); // Remove the existing task
        tasks.Add(item); // Add the updated task to the data source
        XMLTools.SaveListToXMLSerializer<Task>(tasks,s_tasks_xml);
    }
}


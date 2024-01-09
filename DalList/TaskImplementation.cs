namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

/// <summary>
/// Implementation of the data access layer for managing tasks.
/// </summary>
public class TaskImplementation : ITask
{
    /// <summary>
    /// Creates a new task record in the data source.
    /// </summary>
    /// <param name="item">The task to be created.</param>
    /// <returns>The unique identifier of the created task.</returns>
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;
        Task task = new Task();
        task = item with { Id = id };
        DataSource.Tasks.Add(task);
        return item.Id;
    }

    /// <summary>
    /// Deletes a task record from the data source.
    /// </summary>
    /// <param name="id">The unique identifier of the task to be deleted.</param>
    public void Delete(int id)
    {
        Task? task = Read(id); // Search for the task with the specified ID
        if (task == null)
            throw new Exception($"Task with ID={id} does not exist");
        DataSource.Tasks.Remove(task); // Remove the task from the data source
    }

    /// <summary>
    /// Reads a specific task record from the data source.
    /// </summary>
    /// <param name="id">The unique identifier of the task to be retrieved.</param>
    /// <returns>The task with the specified ID, or null if not found.</returns>
    public Task? Read(int id)
    {
        Task? task = DataSource.Tasks.Find(D => D.Id == id); // Search for the task in the list
        if (task == null)
        {
            throw new Exception($"Task with ID={id} does not exist");
        }
        return task;
    }

    /// <summary>
    /// Retrieves all task records from the data source.
    /// </summary>
    /// <returns>A list containing all task records.</returns>
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks); // Create a new list with the values of the original list and return the new list
    }

    /// <summary>
    /// Updates an existing task record in the data source.
    /// </summary>
    /// <param name="item">The updated task information.</param>
    public void Update(Task item)
    {
        if (Read(item.Id) == null)
            throw new Exception($"Task with ID={item.Id} does not exist");

        DataSource.Tasks.Remove(Read(item.Id)!); // Remove the existing task
        DataSource.Tasks.Add(item); // Add the updated task to the data source
    }
}
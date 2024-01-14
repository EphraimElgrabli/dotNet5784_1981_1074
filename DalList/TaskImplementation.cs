namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

/// <summary>
/// Implementation of the data access layer for managing tasks.
/// </summary>
internal class TaskImplementation : ITask
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
        var query = from task in DataSource.Tasks
                    where task.Id == id
                    select task;
        return query.FirstOrDefault();
    }

    public Task? Read(Func<Task, bool> filter)
    {
        return (from task in DataSource.Tasks
                where filter(task)
                select task).FirstOrDefault();
    }

    /// <summary>
    /// Retrieves all task records from the data source.
    /// </summary>
    /// <returns>A list containing all task records.</returns>
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter != null)
        {
            return from task in DataSource.Tasks
                   where filter(task)
                   select task;
        }
        return from task in DataSource.Tasks
               select task;
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
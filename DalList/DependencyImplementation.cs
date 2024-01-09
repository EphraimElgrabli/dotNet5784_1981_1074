namespace Dal;

using System.Collections.Immutable;
using DalApi;
using DO;

/// <summary>
/// Implementation of the data access layer for managing dependencies.
/// </summary>
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// Creates a new dependency record in the data source.
    /// </summary>
    /// <param name="item">The dependency to be created.</param>
    /// <returns>The unique identifier of the created dependency.</returns>
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency dependency = new Dependency();
        dependency = item with { Id = id };
        DataSource.Dependencys.Add(dependency);
        return id;
    }

    /// <summary>
    /// Deletes a dependency record from the data source.
    /// </summary>
    /// <param name="id">The unique identifier of the dependency to be deleted.</param>
    public void Delete(int id)
    {
        Dependency? dependency = Read(id);
        if (dependency == null)
            throw new Exception($"Dependency with ID={id} does not exist");
        DataSource.Dependencys.Remove(dependency); // Remove the dependency from the data source
    }

    /// <summary>
    /// Reads a specific dependency record from the data source.
    /// </summary>
    /// <param name="id">The unique identifier of the dependency to be retrieved.</param>
    /// <returns>The dependency with the specified ID, or null if not found.</returns>
    public Dependency? Read(int id)
    {
        Dependency? dependency = DataSource.Dependencys.Find(D => D.Id == id); // Search for the dependency in the list
        if (dependency == null)
        {
            throw new Exception($"Dependency with ID={id} does not exist");
        }
        return dependency;
    }

    /// <summary>
    /// Retrieves all dependency records from the data source.
    /// </summary>
    /// <returns>A list containing all dependency records.</returns>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

    /// <summary>
    /// Updates an existing dependency record in the data source.
    /// </summary>
    /// <param name="item">The updated dependency information.</param>
    public void Update(Dependency item)
    {
        if (Read(item.Id) == null)
            throw new Exception($"Dependency with ID={item.Id} does not exist");

        DataSource.Dependencys.Remove(Read(item.Id)!); // Remove the existing dependency
        DataSource.Dependencys.Add(item); // Add the updated dependency to the data source
    }
}

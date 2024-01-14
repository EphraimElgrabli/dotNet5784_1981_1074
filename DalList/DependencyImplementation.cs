﻿namespace Dal;

using System.Collections.Immutable;
using System.Collections.Generic;
using DalApi;
using DO;
using System.Linq;

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
        var query = from depend in DataSource.Dependencys
                        where depend.Id == id 
                        select depend;
        return query.FirstOrDefault();
    }

    /// <summary>
    /// Reads a specific dependency record based on a filter from the data source.
    /// </summary>
    /// <param name="">The unique identifier of the dependency to be retrieved.</param>
    /// <returns>The dependency with the specified ID, or null if not found.</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return (from depend in DataSource.Dependencys
               where filter(depend)
               select depend).FirstOrDefault();
    }

    /// <summary>
    /// Retrieves all dependency records from the data source.
    /// </summary>
    /// <returns>A list containing all dependency records.</returns>
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter != null)
        {
            return from depend in DataSource.Dependencys
                   where filter(depend)
                   select depend;
        }
        return from depend in DataSource.Dependencys
               select depend;
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

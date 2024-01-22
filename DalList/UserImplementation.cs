namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

internal class UserImplementation : IUser
{
    /// <summary>
    /// Manages the creation of a new User.
    /// </summary>
    /// <param name="item">The User to be created.</param>
    /// <returns>The ID of the created User.</returns>
    public int Create(User item)
    {
        User? u = DataSource.Users.Find(D => D.Id == item.Id);
        if (u != null)
            throw new DalAlreadyExistException($"User with ID={item.Id} already exists");
        DataSource.Users.Add(item);
        return item.Id;
    }

    /// <summary>
    /// Deletes a User based on the given ID.
    /// </summary>
    /// <param name="id">The ID of the User to be deleted.</param>
    public void Delete(int id)
    {
        User? User = Read(id);
        if (User == null)
            throw new DalDoesNotExistException($"User with ID={id} not exists");
        DataSource.Users.Remove(User);
    }
    public void DeleteAll()
    {
        DataSource.Users.Clear();

    }
    /// <summary>
    /// Reads a User based on the given ID.
    /// </summary>
    /// <param name="id">The ID of the User to be retrieved.</param>
    /// <returns>The User with the specified ID, or null if not found.</returns>
    public User? Read(int id)
    {
        var query = DataSource.Users.FirstOrDefault(u => u.Id == id);
        return query;
    }

    /// <summary>
    /// Reads a User based on the provided filter function.
    /// </summary>
    /// <param name="filter">The filter function to apply.</param>
    /// <returns>The first User matching the filter, or null if not found.</returns>
    public User? Read(Func<User, bool> filter)
    {
        return DataSource.Users.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all Users, optionally filtered by a provided filter function.
    /// </summary>
    /// <param name="filter">The optional filter function.</param>
    /// <returns>An IEnumerable of Users based on the specified filter.</returns>
    public IEnumerable<User> ReadAll(Func<User, bool>? filter = null)
    {
        if (filter != null)
        {
            return from user in DataSource.Users
                   where filter(user)
                   select user;
        }
        return DataSource.Users;
    }

    /// <summary>
    /// Updates a User with new information.
    /// </summary>
    /// <param name="item">The updated User information.</param>
    public void Update(User item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistException($"User with ID={item.Id} not exists");

        DataSource.Users.Remove(Read(item.Id)!);
        DataSource.Users.Add(item);
    }
}


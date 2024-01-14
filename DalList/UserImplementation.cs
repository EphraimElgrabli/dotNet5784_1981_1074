namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

internal class UserImplementation: IUser
{
    public int Create(User item)
    {
        User? u = DataSource.Users.Find(D => D.Id == item.Id);
        if(u != null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Users.Add(item);///the func add item to the list
        return item.Id;
    }

    public void Delete(int id)
    {
        User? User = Read(id);
        if (User == null)
            throw new Exception($"User with ID={id} not exists");
        DataSource.Users.Remove(User);///i search User with id and return the task
    }

    public User? Read(int id)
    {
        var query = from user in DataSource.Users
                    where user.Id == id
                    select user;
        return query.FirstOrDefault();
    }

    public User? Read(Func<User, bool> filter)
    {
        return (from user in DataSource.Users
                where filter(user)
                select user).FirstOrDefault();
    }

    public IEnumerable<User> ReadAll(Func<User, bool>? filter = null)
    {
        if(filter != null)
        {
            return from user in DataSource.Users
                   where filter(user)
                   select user;
        }
        return from user in DataSource.Users
               select user;
    }

    public void Update(User item)
    {
        if (Read(item.Id) == null)
            throw new Exception($"User with ID={item.Id} not exists");

        DataSource.Users.Remove(Read(item.Id)!);/// create new List Engineer with the value of the orginal list and return the new list
        DataSource.Users.Add(item);
    }
}

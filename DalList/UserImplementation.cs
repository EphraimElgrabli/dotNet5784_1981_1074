﻿namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

public class UserImplementation: IUser
{
    public int Create(User item)
    {
        if (Read(item.Id) != null)
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
        User? User = DataSource.Users.Find(D => D.Id == id);///i search for id in the Engineer list
        if (User == null)
        {
            throw new Exception($"User with ID={id} not exists");
        }
        return User;
    }

    public List<User> ReadAll()
    {
        return new List<User>(DataSource.Users);
    }

    public void Update(User item)
    {
        if (Read(item.Id) == null)
            throw new Exception($"User with ID={item.Id} not exists");

        DataSource.Users.Remove(Read(item.Id)!);/// create new List Engineer with the value of the orginal list and return the new list
        DataSource.Users.Add(item);
    }
}
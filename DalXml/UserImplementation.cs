namespace Dal;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using DalApi;
using DO;
internal class UserImplementation : IUser
{
    readonly string s_users_xml = "users";
    public int Create(User item)
    {
        List<User>? users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        User? u = users.Find(D => D.Id == item.Id);
        if (u != null)
            throw new DalAlreadyExistException($"User with ID={item.Id} already exists");
        users.Add(item);
        XMLTools.SaveListToXMLSerializer<User>(users, s_users_xml);
        return item.Id;

    }

    public void Delete(int id)
    {

        User? User = Read(id);
        if (User == null)
            throw new DalDoesNotExistException($"User with ID={id} not exists");
        List<User> users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        users.Remove(User);
        XMLTools.SaveListToXMLSerializer<User>(users, s_users_xml);
    }
    public void DeleteAll()
    {
        List<User> users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        users.Clear();
        XMLTools.SaveListToXMLSerializer<User>(users, s_users_xml);
    }
    public User? Read(int id)
    {
        List<User> users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        var query =users.FirstOrDefault(u => u.Id == id);
        return query;
    }

    public User? Read(Func<User, bool> filter)
    {
        List<User> users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        return users.FirstOrDefault(filter);
    }

    public IEnumerable<User?> ReadAll(Func<User, bool>? filter = null)
    {
        List<User> users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        if (filter != null)
        {
            return from user in users
                   where filter(user)
                   select user;
        }
        return users;
    }

    public void Update(User item)
    {
        
        if (Read(item.Id) == null)
            throw new DalDoesNotExistException($"User with ID={item.Id} not exists");
        List<User> users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        users.Remove(Read(item.Id)!);
        users.Add(item);
        XMLTools.SaveListToXMLSerializer<User>(users, s_users_xml);
    }
}

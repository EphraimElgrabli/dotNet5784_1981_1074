namespace Dal;

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DalApi;
using DO;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencys_xml = "dependencys";

    public int Create(Dependency item)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        int id = Config.NextDependencyId;
        XElement? dep = new XElement("Dependency",
            new XElement("Id", id),
            new XElement("DependentTask", item.DependentTask),
            new XElement("DependsOnTask", item.DependsOnTask));
        root.Add(dep);
        XMLTools.SaveListToXMLElement(root, s_dependencys_xml);
        return item.Id;
    }

    public void Delete(int id)
    {
        Dependency? dep = Read(id); // Search for the task with the specified ID
        if (dep == null)
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist");
        XElement? root = XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().FirstOrDefault(dep => (int?)dep.Element("Id") == id);
        // Remove the task from the data source
        root?.Remove();
        XMLTools.SaveListToXMLElement(root, s_dependencys_xml);
    }

    public Dependency? Read(int id)
    {
        XElement? root = XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().FirstOrDefault(dep => (int?)dep.Element("Id") == id);
        return root is null ? null : GetDependency(root);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(dep => GetDependency(dep)).FirstOrDefault(filter);
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter != null)
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(dep => GetDependency(dep));
        else
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(dep => GetDependency(dep)).Where(filter);
    }

    public void Update(Dependency item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} does not exist");

        XElement root = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        Delete(item.Id); // Remove the existing task
        root.Add(item); // Add the updated task to the data source
        XMLTools.SaveListToXMLElement(root, s_dependencys_xml);
    }


    public Dependency GetDependency(XElement s)
    {
        return new Dependency()
        {
            Id = (int)s.Element("Id"),
            DependsOnTask = (int?)s.Element("DependsOnTask"),
            DependentTask = (int?)s.Element("DependentTask"),
        };
    }
}





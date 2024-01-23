namespace Dal;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Linq;
using DalApi;
using DO;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencys_xml = "dependencys";

    /// <summary>
    /// Creates a new Dependency based on the provided item's data and saves it to the XML data source.
    /// </summary>
    public int Create(Dependency item)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        int id = Config.NextDependencyId;
        // Create a new XML element for Dependency
        XElement? dep = new XElement("Dependency",
            new XElement("Id", id),
            new XElement("DependentTask", item.DependentTask),
            new XElement("DependsOnTask", item.DependsOnTask));
        // Add the new Dependency element to the XML data source
        root.Add(dep);
        // Save the updated XML data back to the file
        XMLTools.SaveListToXMLElement(root, s_dependencys_xml);
        // Return the ID of the newly created Dependency
        return item.Id;
    }

    /// <summary>
    /// Deletes a Dependency with the specified ID from the XML data source.
    /// </summary>
    public void Delete(int id)
    {
        // Search for the Dependency with the specified ID
        Dependency? dep = Read(id);
        // Throw an exception if the Dependency does not exist
        if (dep == null)
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist");
        // Load XML data, find the element with the specified ID, and remove it
        XElement? root = XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().FirstOrDefault(dep => (int?)dep.Element("Id") == id);
        // Remove the Dependency from the data source
        root?.Remove();
        // Save the updated XML data back to the file
        XMLTools.SaveListToXMLElement(root, s_dependencys_xml);
    }

    /// <summary>
    /// Deletes all Dependency elements from the XML data source and resets the ID counter.
    /// </summary>
    public void DeleteAll()
    {
        // Load XML data from file
        XElement root = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        // Remove all Dependency elements
        root.RemoveAll();
        // Save the updated XML data back to the file
        XMLTools.SaveListToXMLElement(root, s_dependencys_xml);
        // Reset the ID counter for the next Dependency to be created
        XMLTools.ResetId("data-config", "NextDependencyId");
    }

    /// <summary>
    /// Reads and retrieves a Dependency with the specified ID from the XML data source.
    /// </summary>
    public Dependency? Read(int id)
    {
        // Load XML data, find the element with the specified ID, and return the Dependency
        XElement? root = XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().FirstOrDefault(dep => (int?)dep.Element("Id") == id);
        return root is null ? null : GetDependency(root);
    }

    /// <summary>
    /// Reads a Dependency based on a custom filter provided as a function.
    /// </summary>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        // Load XML data, select Dependency elements, apply the filter, and return the first matching Dependency
        return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(dep => GetDependency(dep)).FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all Dependency elements from the XML data source, optionally applying a filter.
    /// </summary>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        // Load XML data, select all Dependency elements, and apply the filter if provided
        if (filter != null)
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(dep => GetDependency(dep)).Where(filter);

        else
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(dep => GetDependency(dep));
    }

    /// <summary>
    /// Updates an existing Dependency in the XML data source.
    /// </summary>
    public void Update(Dependency item)
    {
        // Throw an exception if the Dependency with the specified ID does not exist
        if (Read(item.Id) == null)
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} does not exist");

        // Load XML data from file
        XElement root = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        // Remove the existing Dependency
        Delete(item.Id);
        // Add the updated Dependency to the data source
        root.Add(item);
        // Save the updated XML data back to the file
        XMLTools.SaveListToXMLElement(root, s_dependencys_xml);
    }

    /// <summary>
    /// Creates a new Dependency object based on the provided XML element.
    /// </summary>
    public Dependency GetDependency(XElement s)
    {
        // Create a new Dependency and initialize its properties from the XML element
        return new Dependency()
        {
            Id = (int)s.Element("Id"),
            DependsOnTask = (int?)s.Element("DependsOnTask"),
            DependentTask = (int?)s.Element("DependentTask"),
        };
    }
}




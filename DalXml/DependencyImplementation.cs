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
        XElement xmlObj = BuildOrderItemXElemnt(item);
        root.Add(xmlObj);
        XMLTools.SaveListToXMLElement(root, s_dependencys_xml);
        return obj.OrderID;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        var t = root.Elements().FirstOrDefault(dep => (int?)dep.Element("Id") == id);
        return t;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}



using System;
using System.Xml.Linq;
using DalApi;

namespace Dal;

internal class ClockImplementation : IClock
{
    private readonly string s_clock_xml = "data-config";
    public DateTime? GetEndProject()
    {
        XElement root= XMLTools.LoadListFromXMLElement(s_clock_xml).Element("EndProject")!;
        if(root.Value=="")
            return null;
        return DateTime.Parse(root.Value);
    }

    public DateTime? GetStartProject()
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_clock_xml).Element("StartProject")!;
        if (root.Value == "")
            return null;
        return DateTime.Parse(root.Value);


    }

    public void resetTimeProject()
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_clock_xml);
        root.Element("EndProject")!.Value = "";
        root.Element("StartProject")!.Value = "";
        XMLTools.SaveListToXMLElement(root, s_clock_xml);
        
    }

    public DateTime? SetEndProject(DateTime dateTime)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_clock_xml);
        root.Element("EndProject")!.Value = dateTime.ToString();
        XMLTools.SaveListToXMLElement(root, s_clock_xml);
        return dateTime;
    }

    public DateTime? SetStartProject(DateTime dateTime)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_clock_xml);
        root.Element("StartProject")!.Value = dateTime.ToString();
        XMLTools.SaveListToXMLElement(root, s_clock_xml);
        return dateTime;
    }
}

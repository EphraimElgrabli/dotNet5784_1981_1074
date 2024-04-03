

using System;
using System.Xml.Linq;
using DalApi;
namespace Dal;



internal class ClockImplementation : IClock
{
    private readonly string s_clock_xml = "data-config";
    private DalApi.IDal _dal = DalApi.Factory.Get;
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

    public DateTime? SetEndProject()
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_clock_xml);
        //automatic calculation of the end project time
        DateTime? maxTime = DateTime.MinValue; ;
        foreach (var task in _dal.Task.ReadAll())
        {
            if (maxTime < task.DeadlineDate)
            {
                maxTime = task.DeadlineDate;
            }
        }
        
        root.Element("EndProject")!.Value = maxTime.ToString();
        XMLTools.SaveListToXMLElement(root, s_clock_xml);
        return maxTime;
    }

    public DateTime? SetStartProject()
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_clock_xml);
        //automatic calculation of the the start project time
        DateTime? minTime = DateTime.MaxValue; ;

        foreach (var task in _dal.Task.ReadAll())
        {
            if (minTime > task.StartDate)
            {
                minTime = task.StartDate;
            }
        }
        root.Element("StartProject")!.Value = minTime.ToString();
        XMLTools.SaveListToXMLElement(root, s_clock_xml);
        return minTime;
    }
}

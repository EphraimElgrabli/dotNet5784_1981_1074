namespace Dal;

using DO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

/// <summary>
/// Provides tools for XML handling.
/// </summary>
static class XMLTools
{
    const string s_xml_dir = @"..\xml\";

    /// <summary>
    /// Initializes the XMLTools class.
    /// </summary>
    static XMLTools()
    {
        if (!Directory.Exists(s_xml_dir))
            Directory.CreateDirectory(s_xml_dir);
    }

    #region Extension Fuctions

    /// <summary>
    /// Converts the value of the specified XElement to the nullable enumeration of the specified type.
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <param name="element">The XElement to convert.</param>
    /// <param name="name">The name of the child element.</param>
    /// <returns>The nullable enumeration of the specified type.</returns>
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;

    /// <summary>
    /// Converts the value of the specified XElement to the nullable DateTime.
    /// </summary>
    /// <param name="element">The XElement to convert.</param>
    /// <param name="name">The name of the child element.</param>
    /// <returns>The nullable DateTime.</returns>
    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;

    /// <summary>
    /// Converts the value of the specified XElement to the nullable double.
    /// </summary>
    /// <param name="element">The XElement to convert.</param>
    /// <param name="name">The name of the child element.</param>
    /// <returns>The nullable double.</returns>
    public static double? ToDoubleNullable(this XElement element, string name) =>
        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;

    /// <summary>
    /// Converts the value of the specified XElement to the nullable integer.
    /// </summary>
    /// <param name="element">The XElement to convert.</param>
    /// <param name="name">The name of the child element.</param>
    /// <returns>The nullable integer.</returns>
    public static int? ToIntNullable(this XElement element, string name) =>
        int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;

    #endregion

    #region XmlConfig

    /// <summary>
    /// Gets and increases the next identifier from the specified XML data.
    /// </summary>
    /// <param name="data_config_xml">The XML data configuration.</param>
    /// <param name="elemName">The name of the element.</param>
    /// <returns>The next identifier.</returns>
    public static int GetAndIncreaseNextId(string data_config_xml, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        int nextId = root.ToIntNullable(elemName) ?? throw new FormatException($"can't convert id.  {data_config_xml}, {elemName}");
        root.Element(elemName)?.SetValue((nextId + 1).ToString());
        XMLTools.SaveListToXMLElement(root, data_config_xml);
        return nextId;
    }

    /// <summary>
    /// Resets the identifier to 1000 in the specified XML data.
    /// </summary>
    /// <param name="data_config_xml">The XML data configuration.</param>
    /// <param name="elemName">The name of the element.</param>
    public static void ResetId(string data_config_xml, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        root.Element(elemName)?.SetValue((1000).ToString()); // reset id to 1000
        XMLTools.SaveListToXMLElement(root, data_config_xml);
    }

    #endregion

    #region SaveLoadWithXElement

    /// <summary>
    /// Saves the XElement to an XML file.
    /// </summary>
    /// <param name="rootElem">The root XElement.</param>
    /// <param name="entity">The entity name.</param>
    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }

    /// <summary>
    /// Loads the XElement from an XML file.
    /// </summary>
    /// <param name="entity">The entity name.</param>
    /// <returns>The loaded XElement.</returns>
    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }

    #endregion

    #region SaveLoadWithXMLSerializer

    /// <summary>
    /// Saves the list to an XML file using XML serialization.
    /// </summary>
    /// <typeparam name="T">The type of objects in the list.</typeparam>
    /// <param name="list">The list to save.</param>
    /// <param name="entity">The entity name.</param>
    public static void SaveListToXMLSerializer<T>(List<T> list, string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            new XmlSerializer(typeof(List<T>)).Serialize(file, list);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    
    /// <summary>
    /// Loads the list from an XML file using XML serialization.
    /// </summary>
    /// <typeparam name="T">The type of objects in the list.</typeparam>
    /// <param name="entity">The entity name.</param>
    /// <returns>The loaded list.</returns>
    public static List<T> LoadListFromXMLSerializer<T>(string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T>));
            return x.Deserialize(file) as List<T> ?? new();
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {filePath}, {ex.Message}");
        }
    }

    #endregion
}
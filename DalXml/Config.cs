namespace Dal;

internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextCourseId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextCourseId"); }
    internal static int NextLinkId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextLinkId"); }
}

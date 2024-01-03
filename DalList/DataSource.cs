namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        
    }
    internal static List<DO.Config> Configs { get; } = new();
    internal static List<DO.Dependency> Dependencys { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
}

namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        /// <summary>
        /// if i unerstood correctly the instructions there should be
        /// an implementation for two runing numbers, one for the tasks
        /// and one for the dependncy, there isnt one for the engineer since
        /// his is is already unique
        /// </summary>
        
        //Task id running number
        internal const int startTaskId = 1000;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }


        // DependncyID runing number
        internal const int startDependencyId = 1000;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }
    }
    internal static List<DO.Config?> Configs { get; } = new();
    internal static List<DO.Dependency?> Dependencys { get; } = new();
    internal static List<DO.Engineer?> Engineers { get; } = new();
    internal static List<DO.Task?> Tasks { get; } = new();
}

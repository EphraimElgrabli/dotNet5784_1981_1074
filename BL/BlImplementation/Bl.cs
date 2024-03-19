    namespace BlImplementation;
    using BlApi;
    internal class Bl : IBl
    {
        public IUser User => new UserImplementation();
        public ITask Task => new TaskImplemtation();
        public IClock Clock => new ClockImplementation();
    }

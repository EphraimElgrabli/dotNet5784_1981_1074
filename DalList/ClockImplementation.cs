using DalApi;

namespace Dal;

internal class ClockImplementation : IClock
{
    public DateTime? GetEndProject()
    {
        return DataSource.Config.EndDate;
    }

    public DateTime? GetStartProject()
    {
        return DataSource.Config.StartDate;
    }

    public void resetTimeProject()
    {
         DataSource.Config.StartDate = DataSource.Config.EndDate = null;
    }

    public DateTime? SetEndProject(DateTime dateTime)
    {
        DataSource.Config.EndDate=dateTime;
        return DataSource.Config.EndDate;
    }

    public DateTime? SetStartProject(DateTime dateTime)
    {
        DataSource.Config.StartDate=dateTime;
        return DataSource.Config.StartDate;
    }
}

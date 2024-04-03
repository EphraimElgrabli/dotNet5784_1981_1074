using DalApi;

namespace Dal;

internal class ClockImplementation : IClock
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
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

    public DateTime? SetEndProject()
    {
        DateTime? maxTime = DateTime.MinValue; ;
        foreach (var task in _dal.Task.ReadAll())
        {
            if (maxTime < task.DeadlineDate)
            {
                maxTime = task.DeadlineDate;
            }
        }
        DataSource.Config.EndDate=maxTime;
        return DataSource.Config.EndDate;
    }

    public DateTime? SetStartProject()
    {
        DateTime? minTime = DateTime.MaxValue; ;

        foreach (var task in _dal.Task.ReadAll())
        {
            if (minTime > task.StartDate)
            {
                minTime = task.StartDate;
            }
        }
        DataSource.Config.StartDate=minTime;
        return DataSource.Config.StartDate;
    }
}

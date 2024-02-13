using BO;

namespace BlImplementation;

internal class ClockImplementation : BlApi.IClock
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    public DateTime? GetStartProject()=>_dal.Clock.GetStartProject();
    
    public DateTime? GetEndProject()=>_dal.Clock.GetEndProject();
    
    public DateTime? SetStartProject(DateTime startdateTime)=>_dal.Clock.SetStartProject(startdateTime);
    
    public DateTime? SetEndProject(DateTime enddateTime)=>_dal.Clock.SetEndProject(enddateTime);

    public StatusProject GetStatusProject()
    {
        if (GetEndProject() < DateTime.Now)
            return StatusProject.Done;
        if (GetStartProject() < DateTime.Now)
            return StatusProject.InProgress;
        return StatusProject.UnStarted;

    }

    public void resetTimeProject()=>_dal.Clock.resetTimeProject();
}

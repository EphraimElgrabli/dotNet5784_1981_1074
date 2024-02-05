using BO;

namespace BlApi;

public interface IMilestone
{
    public int Create(Milestone mstone);
    public void Delete(int id);
    public void Update(Milestone mstone);
    public IEnumerable<MilestoneInList> ReadAll();
    public Milestone Read(int id);
}
//not sure i will implement it, look complicated AF,
//i coudenlt understand by myself what does milestone in list mean,
//wasn't written in the manuel
namespace BlApi;

public interface IUser
{
    public int Create(BO.User user);
    public BO.User? Read(int id);
    public IEnumerable<BO.UserInTask> ReadAll(Func<BO.UserInTask, bool>? filter = null);
    public void Update(BO.User user);
    public void Delete(int id);
}
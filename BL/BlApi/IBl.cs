namespace BlApi;

public interface IBl
{
    public IUser User { get; }
    public ITask Task { get; }
    public IClock Clock { get; }

    #region Time and Clock
    public DateTime DateNow { get; }
    void DeleteAll();
    void PrometeSecond();
    void PrometeMinute();
    void PrometeHour();
    void PrometeDay();
    void PrometeMonth();
    void PrometeYear();
    void initalizeTime();
    #endregion
}

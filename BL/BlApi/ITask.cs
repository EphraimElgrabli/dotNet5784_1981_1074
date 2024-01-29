﻿namespace BlApi;

public interface ITask
{
    public int Create(BO.Task task);
    public BO.Task? Read(int id);
    public IEnumerable<BO.TaskInList> ReadAll();
    public void Update(BO.Task task);
    public void Delete(int id);
    public void UpdateDates(int id, DateTime date);

}
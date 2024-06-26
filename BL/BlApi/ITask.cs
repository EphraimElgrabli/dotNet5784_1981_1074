﻿using BO;

namespace BlApi;

public interface ITask
{
    public int Create(BO.Task task);
    public BO.Task? Read(int id);
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null);
    public IEnumerable<BO.Task> ReadAllTask(Func<BO.Task, bool>? filter = null);
    public void Update(BO.Task task);
    public void Delete(int id);
    public void UpdateDates(int id, DateTime date);
    public void promoteStatusTask(int id);
    public IEnumerable<BO.Task> changeTaskList(IEnumerable<BO.TaskInUser> tasks);
    public DateTime CalculateStartTime(BO.Task task);
    public BO.Status CalculateStatus(int id);
    public void GanttTime();
    public UserInTask? userintask(int? id);

}
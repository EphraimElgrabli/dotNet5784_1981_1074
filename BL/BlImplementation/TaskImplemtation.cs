using BlApi;
using BO;
using DalApi;
namespace BlImplementation;
/// <summary>
/// Implementation of the ITask interface for managing tasks.
/// </summary>
internal class TaskImplemtation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private readonly IBl _bl;
    internal TaskImplemtation(IBl bl) => _bl = bl;

    /// <summary>
    /// Calculates the status of a task based on its attributes.
    /// </summary>
    /// <param name="id">The ID of the task to calculate the status for.</param>
    /// <returns>The status of the task.</returns>
    public BO.Status CalculateStatus(int id)
    {
        BO.Task? task = Read(id);

        DateTime currentDate = DateTime.Now.AddMonths(6);

        if (task.ScheduledDate > currentDate)
        {
            return BO.Status.Unscheduled;
        }
        else if (task.StartDate > currentDate)
        {
            return BO.Status.Scheduled;
        }
        else if (task.CompleteDate > currentDate)
        {
            if (task.DeadlineDate < currentDate)
            {
                return BO.Status.InJeopardy;
            }
            else
            {
                return BO.Status.OnTrack;
            }
        }
        else
        {
            return BO.Status.Done;
        }
    }
    /// <summary>
    /// Creates a new task.
    /// </summary>
    /// <param name="doTask">The task to create.</param>
    /// <returns>The ID of the created task.</returns>
    public int Create(BO.Task doTask)
    {
        
            if (doTask.Alias == "") throw new BO.BlValueNotExistException("Alias is empty");
            
            if (doTask.User != null)
            {
                if (_dal.User.Read(doTask.User.Id) == null) throw new BO.BlDoesNotExistException($"User with ID={doTask.User.Id} does Not exist");
            }
        try
        {
            doTask.Dependencies?.Select(d => _dal.Dependency.Create(new(0, d.Id, doTask.Id)));
            int idTask = _dal.Task.Create(new DO.Task()
            {
                Id = doTask.Id,
                Description = doTask.Description,
                Alias = doTask.Alias,
                CreatedAtDate = doTask.CreatedAtDate,
                StartDate = doTask.StartDate,
                ScheduledDate = doTask.ScheduledDate,
                DeadlineDate = doTask.DeadlineDate,
                cost = doTask.Cost,
                CompleteDate = doTask.CompleteDate,
                Deliverables = doTask.Deliverables,
                Remarks = doTask.Remarks,
                Complexity = (DO.UserLevel?)doTask.Status,
                UserId = doTask.User?.Id,
                IsMilestone = (doTask.Milestone?.Id == null) ? false : true,
            });
            if (doTask.User.Id != null)
            {
                //all this because i do in User a list of tasks and here i add the task to the list
                BO.User? t = _bl.User.Read(doTask.User.Id);

                BO.TaskInUser temp = new BO.TaskInUser() { Id = doTask.Id, Alias = doTask.Alias };

                if (t.Tasks == null) t.Tasks.Add(temp);
                else
                {
                    if (t.Tasks.Find(t => t.Id == doTask.Id).Id != temp.Id)
                    {
                        t.Tasks.Add(temp);
                    }
                }
                 _bl.User.Update(t);
            }
            return idTask;

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"User with ID={doTask.Id} does not exist", ex);
        }

    }
    /// <summary>
    /// Deletes a task.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    public void Delete(int id)
    {
        try { _dal.Task.Delete(id); }
        catch (DO.CanNotDeletedException ex)
        {
            throw new BO.CanNotDeletedException($"Task with ID={id} can not deleted", ex);
        }

    }
    /// <summary>
    /// Reads a task based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the task to read.</param>
    /// <returns>The task with the specified ID.</returns>
    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        UserInTask? u = userintask(doTask.UserId);
        return new BO.Task()
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CreatedAtDate,
            StartDate = doTask.StartDate,
            ScheduledDate = doTask.ScheduledDate,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Deliverables = doTask.Deliverables,
            Cost=doTask.cost,
            Remarks = doTask.Remarks,
            User= u,
            Complexity = (BO.UserLevel)doTask.Complexity,
            Dependencies = GetAllDependencie(doTask.Id),
           
        };
    }

    /// <summary>
    /// Reads all tasks, optionally filtered by a provided filter function.
    /// </summary>
    /// <param name="filter">The optional filter function.</param>
    /// <returns>An IEnumerable of tasks based on the specified filter.</returns>
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null)
    {
        if (filter == null)
        {
            return (from DO.Task doTask in _dal.Task.ReadAll()
                    select new BO.TaskInList
                    {
                        Id = doTask.Id,
                        Description = doTask.Description,
                        Alias = doTask.Alias
                    });
        }
        else
        {
            return (from DO.Task doTask in _dal.Task.ReadAll()
                    let boTask = new BO.TaskInList
                    {
                        Id = doTask.Id,
                        Description = doTask.Description,
                        Alias = doTask.Alias
                    }
                    where filter(boTask)
                    select boTask);

        }
    }
    public UserInTask? userintask(int? id)
    {
        if (id == null) return null;

        foreach(var v in _bl.User.ReadAllUser())
        {
           
                    if (v.Id == id)
                    {
                        return new UserInTask() { Id = v.Id, Name = v.Name };
                    }
                
            
        }
        return null;
     /*   BO.Task u = Read((int)id)!;
        return new UserInTask()
        {
            Id = u.Id,
            Name = u.Alias
        };
     */
    }

    /// <summary>
    /// Reads all tasks, optionally filtered by a provided filter function.
    /// </summary>
    /// <param name="filter">The optional filter function.</param>
    /// <returns>An IEnumerable of tasks based on the specified filter.</returns>
    public IEnumerable<BO.Task> ReadAllTask(Func<BO.Task, bool>? filter = null)
    {

        if (filter == null)
        {

            return (from DO.Task doTask in _dal.Task.ReadAll()

                    select new BO.Task
                    {
                        Id = doTask.Id,
                        Description = doTask.Description,
                        Alias = doTask.Alias,
                        CreatedAtDate = doTask.CreatedAtDate,
                        StartDate = doTask.StartDate,
                        Status=CalculateStatus(doTask.Id),
                        ScheduledDate = doTask.ScheduledDate,
                        DeadlineDate = doTask.DeadlineDate,
                        CompleteDate = doTask.CompleteDate,
                        Deliverables = doTask.Deliverables,
                        Cost=doTask.cost,
                        Remarks = doTask.Remarks,
                        User = userintask(doTask.UserId),
                        Dependencies = GetAllDependencie(doTask.Id),
                        pracentstart = doTask.pracentstart,
                        pracentbetween = doTask.pracentbetween,
                        pracentend = doTask.pracentend
                    });;
        }
        else
        {
            return (from DO.Task doTask in _dal.Task.ReadAll()
                    let boTask = new BO.Task
                    {
                        Id = doTask.Id,
                        Description = doTask.Description,
                        Alias = doTask.Alias,
                        CreatedAtDate = doTask.CreatedAtDate,
                        StartDate = doTask.StartDate,
                        Status = CalculateStatus(doTask.Id),
                        ScheduledDate = doTask.ScheduledDate,
                        DeadlineDate = doTask.DeadlineDate,
                        CompleteDate = doTask.CompleteDate,
                        Deliverables = doTask.Deliverables,
                        Cost=doTask.cost,
                        Remarks = doTask.Remarks,
                        User = userintask(doTask.UserId),
                        Dependencies = GetAllDependencie(doTask.Id)
                    }
                    where filter(boTask)
                    select boTask);
        }
    }
    public List<BO.TaskInList> GetAllDependencie(int id)
    {
        IEnumerable<BO.TaskInList> temp = from DO.Dependency d in _dal.Dependency.ReadAll()
                                          where d.DependentTask == id
                                          let t = _dal.Task.Read(d.DependsOnTask)
                                          select new BO.TaskInList
                                          {
                                              Id = t.Id,
                                              Description = t.Description,
                                              Alias = t.Alias

                                          };
        return temp.ToList();
    }
    /// <summary>
    /// Updates a task with new information.
    /// </summary>
    /// <param name="task">The updated task information.</param>
    public void Update(BO.Task task)
    {
        if (task.Alias == "") throw new BO.BlValueNotExistException("Alias is empty");
        try
        {
            _dal.Task.Update(new DO.Task()
            {
                Id = task.Id,
                Description = task.Description,
                Alias = task.Alias,
                pracentstart = task.pracentstart,
                pracentbetween = task.pracentbetween,
                pracentend = task.pracentend,
                CreatedAtDate = task.CreatedAtDate,
                StartDate = task.StartDate,
                ScheduledDate = task.ScheduledDate,
                DeadlineDate = task.DeadlineDate,
                CompleteDate = task.CompleteDate,
                Deliverables = task.Deliverables,
                Remarks = task.Remarks,
                cost=task.Cost,
                Complexity = (DO.UserLevel?)task.Status,
                UserId = task.User?.Id,
                IsMilestone = (task.Milestone?.Id == null) ? false : true,
            });
            if (task.User != null)
            {
                //all this because i do in User a list of tasks and here i add the task to the list
                BO.User? t = _bl.User.Read(task.User.Id);

                BO.TaskInUser temp = new BO.TaskInUser() { Id = task.User.Id, Alias = task.User.Name };
                if (t.Tasks == null)
                {
                    t.Tasks = new List<TaskInUser>();
                    t.Tasks.Add(temp);
                }
                else
                {
                    if (t.Tasks.Find(t => t.Id == task.Id).Id != temp.Id)
                    {
                        t.Tasks.Add(temp);
                    }
                }
                _bl.User.Update(t);
            }
            if (task.Dependencies != null)
            {
                foreach (DO.Dependency dependency in _dal.Dependency.ReadAll(d => d.DependentTask == task.Id))
                { _dal.Dependency.Delete(dependency.Id); }

                foreach (var d in task.Dependencies)
                {
                    _dal.Dependency.Create(new DO.Dependency(0, d.Id, task.Id));
                }
            }
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message);
        }

    }
    /// <summary>
    /// Updates the dates of a task.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="date">The new date to set.</param>
    public void UpdateDates(int id, DateTime date)
    {
        try
        {
            BO.Task? task = Read(id);
            if (task != null)
            {
                if (task.Dependencies != null)
                {
                    foreach (BO.TaskInList taskin in task.Dependencies)
                    {
                        BO.Task? task1 = Read(taskin.Id);
                        if (task1 != null && task1.StartDate == null && task1.ScheduledDate < date)
                            throw new BO.BlDateProblemException($"Task with ID={task1.Id} does Not have start date");


                    }
                }

                task.StartDate = date;
                Update(task);
            }
        }
        catch (BO.BlDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        }

    }
    /// <summary>
    /// //Updates the status of a task.
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlDateProblemException"></exception>
    public DateTime CalculateStartTime(BO.Task task)
    {

        DateTime? minTime = _dal.Clock.GetStartProject();          
        foreach (var d in task.Dependencies)
        {
            BO.Task? task1 = Read(d.Id);
            if (task1.DeadlineDate == null)
                throw new BO.BlDateProblemException($"TIme of Task with id: {task.Id} can not update, it depend on task {task1.Id}");
            if (task1 != null && task1.DeadlineDate != null)
                if(minTime< task1.DeadlineDate)
                    minTime= Convert.ToDateTime(task1.DeadlineDate);

        }
        return Convert.ToDateTime(minTime);
    }
    public IEnumerable<BO.Task> changeTaskList(IEnumerable<BO.TaskInUser>? tasks)
    {
        List<BO.Task> temp = new List<BO.Task>();
        foreach (var t in tasks)
        {
            temp.Add(Read(t.Id)!);//add the task to the list of
        }
        return temp;    
    }
    public void GanttTime() 
    {
        DateTime? minTime = DateTime.MaxValue; ;
        DateTime? maxTime = DateTime.MinValue; ;
        DateTime? start = _dal.Clock.GetStartProject();
        //if (start == null)
        //{
        //    start = DateTime.Now;
        //    minTime = DateTime.Now;
        //    maxTime = DateTime.Now;
        //}
        //else
        //{
        //    minTime = _dal.Clock.GetStartProject();
        //    maxTime = _dal.Clock.GetStartProject();
        //    start = _dal.Clock.GetStartProject();
        //}
        foreach (var task in ReadAllTask())
        {
            if (minTime > task.StartDate)
            {
                minTime= task.StartDate;
            }
        }
        foreach (var task in ReadAllTask())
        {
            if (maxTime < task.DeadlineDate)
            {
                maxTime = task.DeadlineDate;
            }
        }
        foreach (var task in ReadAllTask())
        {
            double totalHrs = (maxTime - minTime).Value.TotalHours;
            double startHrs = (task.StartDate - minTime).Value.TotalHours;
            double endHrs = (task.DeadlineDate - minTime).Value.TotalHours;

            task.pracentstart = (int)(startHrs / totalHrs * 100);
            task.pracentend = (int)((totalHrs - endHrs) / totalHrs * 100);
            task.pracentbetween = 100 - task.pracentend - task.pracentstart;

            Update(task);
        }
    }

    /// <summary>
    /// //Updates the status of a task.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    public  void promoteStatusTask(int id)
    {

        if (Read(id) == null) throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        BO.Task task = Read(id)!;
        if (task.Status == BO.Status.Scheduled)
        {

            foreach (var d in task.Dependencies!)
            {
                if (Read(d.Id).Status != BO.Status.Done)
                {
                    throw new BO.BlAlreadyExistException("The task can not promote,Dependencies task not finish");
                }
            }
            if (task.User == null)
            {
                throw new BO.BlAlreadyExistException("The task can not promote,User not exist");
            }
            if (_bl.User.Read(task.User!.Id)!.Tasks!.Any(d => Read(d.Id).Status != BO.Status.OnTrack))
            {
                throw new BO.BlAlreadyExistException("The task can not promote,other task already Ontrack");
            }
            task.Status = BO.Status.OnTrack;
        }
        else if (task.Status == BO.Status.OnTrack)
        {
            task.Status = BO.Status.Done;
        }
        else
        {
            throw new BO.BlAlreadyExistException("The task can not promote");
        }

    }
    public int GetCost()
    {
        int cost = 0;
        foreach (var task in ReadAllTask())
        {
            cost += task.Cost;
        }
        return cost;
    }
}

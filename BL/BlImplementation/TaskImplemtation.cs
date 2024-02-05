using DO;

namespace BlImplementation;

internal class TaskImplemtation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task doTask)
    {


        try
        {
            if (doTask.Alias == "") throw new BO.BlValueNotExistException("Alias is empty");
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
                CompleteDate = doTask.CompleteDate,
                Deliverables = doTask.Deliverables,
                Remarks = doTask.Remarks,
                Complexity = (DO.Status?)doTask.Status,
                UserId = doTask.User?.Id,
                IsMilestone = (doTask.Milestone?.Id == null) ? false : true,
            });

            return idTask;

        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"Task with ID={doTask.Id} does not exist");
        }

    }

    public void Delete(int id)
    {
        try { _dal.Task.Delete(id); }
        catch (DO.CanNotDeletedException ex)
        {
            throw new BO.CanNotDeletedException($"Task with ID={id} can not deleted");
        }

    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        return new BO.Task()
        {

            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CreatedAtDate,
            Status = (BO.Status)doTask.Complexity,
            StartDate = doTask.StartDate,
            ScheduledDate = doTask.ScheduledDate,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Complexity = (BO.UserLevel?)doTask.Complexity,

        };
    }

    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null)
    {
        if (filter == null)
        {
            return (from DO.Task doTask in _dal.Task.ReadAll()
                    select new BO.TaskInList
                    {
                        Id = doTask.Id,
                        Description = doTask.Description,
                        Alias = doTask.Alias,
                        Status = (BO.Status)doTask.Complexity,
                    });
        }
        else
        {
            return (from DO.Task doTask in _dal.Task.ReadAll()
                    let boTask = new BO.TaskInList
                    {
                        Id = doTask.Id,
                        Description = doTask.Description,
                        Alias = doTask.Alias,
                        Status = (BO.Status)doTask.Complexity,
                    }
                    where filter(boTask)
                    select boTask);

        }
    }

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
                CreatedAtDate = task.CreatedAtDate,
                StartDate = task.StartDate,
                ScheduledDate = task.ScheduledDate,
                DeadlineDate = task.DeadlineDate,
                CompleteDate = task.CompleteDate,
                Deliverables = task.Deliverables,
                Remarks = task.Remarks,
                Complexity = (DO.Status?)task.Status,
                UserId = task.User?.Id,
                IsMilestone = (task.Milestone?.Id == null) ? false : true,
            });
            foreach (DO.Dependency dependency in _dal.Dependency.ReadAll(d => d.DependsOnTask == task.Id)) { _dal.Dependency.Delete(dependency.Id); }

            foreach (var d in task.Dependencies)
            {
                _dal.Dependency.Create(new DO.Dependency(0, d.Id, task.Id));
            }

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} does Not exist");
        }

    }

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
                        if (task1 != null && task1.StartDate == null && task1.ScheduledDate < date) throw new BO.BlDateProblemException($"Task with ID={task1.Id} does Not have start date");


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
}

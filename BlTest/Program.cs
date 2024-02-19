using BO;
namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();

    /// <summary>
    /// //entity menu
    /// </summary>
    /// <param name="ch"></param>
    static void EntityMenu(string ch)
    {
        Console.WriteLine("0: exit");
        Console.WriteLine($"1: delete a {ch}");
        Console.WriteLine($"2: create a {ch}");
        Console.WriteLine($"3: update a {ch}");
        Console.WriteLine($"4: print all the {ch}s");
        Console.WriteLine($"5: search for a {ch}");

    }
    /// <summary>
    /// //Dependency menu
    /// </summary>
    static void dependecymenu()
    {
        Console.WriteLine("0: exit");
        Console.WriteLine("1: Create Dependency");
        Console.WriteLine("2: print all the Dependencies");
        Console.WriteLine("3: search for a Dependency");
    }
    //  ########################
    //  Printing functions
    //  ########################
    //  Print the main menu
    /// <summary>
    /// //Main menu
    /// </summary>
    static void MainMenu()
    {
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0: exit");
        Console.WriteLine("1: User menu");
        Console.WriteLine("2: Task menu");
        Console.WriteLine("3: Dependency menu");
        Console.WriteLine("4: update dates");
    }
    /// <summary>
    /// //Stage 1 of the project
    /// </summary>
    public static void Stage1project()
    {
        MainMenu();
        int choice = int.Parse(Console.ReadLine()!);
        int choice2;
        while (choice != 0)
        {

            switch (choice)
            {
                case 0:
                    break;
                case 1:

                    EntityMenu("User");
                    choice2 = int.Parse(Console.ReadLine()!);
                    while (choice2 != 0)
                    {
                        try
                        {
                            switch (choice2)
                            {
                                case 0: break;
                                case 1: deleteUser(s_bl.User); break;
                                case 2: createUser(s_bl.User); break;
                                case 3: updateUser(s_bl.User); break;
                                case 4: printAllUsers(s_bl.User); break;
                                case 5: searchUser(s_bl.User); break;
                                default:
                                    Console.WriteLine("Incorrect input, plese press a number from 0 to 5");
                                    break;
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        EntityMenu("User");
                        choice2 = int.Parse(Console.ReadLine()!);

                    }
                    break;
                case 2:
                    EntityMenu("Task");
                    choice2 = int.Parse(Console.ReadLine()!);

                    while (choice2 != 0)
                    {
                        try
                        {
                            switch (choice2)
                            {
                                case 0: break;
                                case 1: deleteTask(s_bl.Task); break;
                                case 2: createTask(s_bl.Task); break;
                                case 3: updateTask(s_bl.Task); break;
                                case 4: printAllTasks(s_bl.Task); break;
                                case 5: searchTask(s_bl.Task); break;
                                default:
                                    Console.WriteLine("Incorrect input, plese press a number from 0 to 5");
                                    break;
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        EntityMenu("Task");
                        choice2 = int.Parse(Console.ReadLine()!);
                    }
                    break;
                case 3:
                    dependecymenu();
                    choice2 = int.Parse(Console.ReadLine()!);
                    while (choice2 != 0)
                    {
                        try
                        {
                            switch (choice2)
                            {
                                case 0: break;
                                case 1: CreateDependency(); break;
                                case 2: printAllDependencies(); break;
                                case 3: searchDependency(int.Parse(GetString("Enter the id of the task"))); break;
                                default:
                                    Console.WriteLine("Incorrect input, plese press a number from 0 to 3");
                                    break;
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        dependecymenu();
                        choice2 = int.Parse(Console.ReadLine()!);
                    }
                    break;
                case 4:
                    Console.WriteLine("enter the start date of the project");
                    DateTime start = DateTime.Parse(Console.ReadLine()!);
                    s_bl.Clock.SetStartProject(start);
                    Console.WriteLine("Now you are about to set dates for tasks");
                    foreach (var task in s_bl.Task.ReadAllTask())
                    {
                        try
                        {
                            string answer = GetString($"Do you want to assign task with id {task.Id} to a user?\nIf Enter N The program will calculate the upcoming time\n(Y/N)");
                            if (answer == "Y" || answer == "y")
                            {
                                Schedule();
                            }
                            else
                            {
                                DateTime date = s_bl.Task.CalculateStartTime(task);
                                Console.WriteLine($"The start time of task {task.Id} is {date}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    return;

                    break;
                default:
                    Console.WriteLine("Incorrect input, plese press a number from 1 to 4");
                    break;
            }

            MainMenu();
            choice = int.Parse(Console.ReadLine()!);
        }
    }
    /// <summary>
    /// Displays the menu options for managing dependencies.
    /// </summary>
    static void CreateDependency()
    {
        int menu = int.Parse(GetString("0: exit\n1: Create Dependency"));
        while (menu != 0)
        {
            try
            {

                int id = int.Parse(GetString("Enter the id of the task"));
                if (s_bl?.Task.Read(id) == null) throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
                int previousTaskId = int.Parse(GetString("Enter the Id of the previous task:"));
                if (s_bl?.Task.Read(previousTaskId) == null) throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
                TaskInList? dependencies = new TaskInList() { Id = previousTaskId, Description = s_bl.Task.Read(previousTaskId)?.Description, Alias = s_bl.Task.Read(previousTaskId)?.Alias, Status = s_bl.Task.Read(previousTaskId)!.Status };
                s_bl?.Task.Read(id)!.Dependencies?.Add(dependencies);
                s_bl?.Task.Update(s_bl.Task.Read(id)!);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            menu = int.Parse(GetString("0: exit\n1: Create Dependency"));
        }

    }
    /// <summary>
    /// //User menu
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    static void searchDependency(int id)
    {

        if (s_bl?.Task.Read(id) == null) throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        Console.WriteLine($"The Dependencies of task {id} is:");
        foreach (var v in s_bl?.Task.Read(id)!.Dependencies)
        {
            Console.WriteLine(v);
            Console.WriteLine("\n");
        }
    }
    /// <summary>
    /// //User menu
    /// </summary>
    static void printAllDependencies()
    {
        foreach (var task in s_bl!.Task.ReadAllTask())
        {
            searchDependency(task.Id);
        }
    }



    //###############
    //crud functions
    //###############

    //Useful Function
    /// <summary>
    /// //Useful Function
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    static string GetString(string s)
    {
        Console.WriteLine(s);
        return Console.ReadLine()!;
    }
    //User crud functions
    /// <summary>
    /// //User crud functions
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    static BO.User getUser()
    {
        // reciveing input of credentials from the user
        Console.WriteLine("Enter Id:");
        int id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter name:");
        string name = Console.ReadLine()!;
        Console.WriteLine("Enter email:");
        string email = Console.ReadLine()!;
        Console.WriteLine("Enter phone number:");
        string phone = Console.ReadLine()!;
        Console.WriteLine("Enter User level:");
        BO.UserLevel level = Enum.Parse<BO.UserLevel>(Console.ReadLine()!);

        // Create a new Engineer object with the updated details
        return new BO.User()
        {
            Id = id,
            Name = name,
            Email = email,
            PhoneNumber = phone,
            Level = level
        };
    }
    /// <summary>
    /// //User crud functions
    /// </summary>
    /// <param name="s_dalUser"></param>
    static void createUser(BlApi.IUser? s_dalUser)
    {
        s_dalUser?.Create(getUser());

    }
    /// <summary>
    /// //User crud functions
    /// </summary>
    /// <param name="s_dalUser"></param>
    static void updateUser(BlApi.IUser? s_dalUser)
    {
        s_dalUser?.Update(getUser());
    }
    /// <summary>
    /// //User crud functions
    /// </summary>
    /// <param name="s_dalUser"></param>
    static void deleteUser(BlApi.IUser? s_dalUser)
    {
        Console.WriteLine("Enter Id:");
        int id = int.Parse(Console.ReadLine()!);
        s_dalUser?.Delete(id);
    }
    /// <summary>
    /// //User crud functions
    /// </summary>
    /// <param name="s_dalUser"></param>
    static void printAllUsers(BlApi.IUser? s_dalUser)
    {
        foreach (var user in s_bl!.User.ReadAllUser()) { Console.WriteLine(user); }
    }
    /// <summary>
    /// //User crud functions
    /// </summary>
    /// <param name="s_dalUser"></param>
    static void searchUser(BlApi.IUser? s_dalUser)
    {
        Console.WriteLine("Enter Id:");
        int id = int.Parse(Console.ReadLine()!);
        BO.User? user = s_dalUser?.Read(id);
        Console.WriteLine(user);
    }

    //Task crud functions
    /// <summary>
    /// //Task crud functions
    /// </summary>
    /// <param name="isUpdate"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    static BO.Task getTask(bool isUpdate)
    {
        int id;
        if (isUpdate)
        {
            Console.WriteLine("Enter Id: ");
            id = int.Parse(Console.ReadLine()!);
            if (s_bl?.Task.Read(id) == null) throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        }
        else
        {
            id = 0;
        }
        // Prompt the user to enter updated details for the Task
        string alias = GetString("Enter alias:");
        string description = GetString("Enter description:");
        int cost = int.Parse(GetString("Enter cost:"));
        DateTime createAtDate = DateTime.Now;
        Console.WriteLine("Enter Complexity:");
        BO.UserLevel level = Enum.Parse<BO.UserLevel>(Console.ReadLine()!);
        string deliverables = GetString("Enter deliverables:");
        string remarks = GetString("Enter remarks:");

        // List<TaskInList> dependencies = new List<TaskInList>();




        // Create a new Task object with the updated details
        return new BO.Task()
        {

            Id = id,
            Alias = alias,
            Description = description,
            Cost = cost,
            CreatedAtDate = createAtDate,
            Complexity = level,
            Deliverables = deliverables,
            Remarks = remarks,
            // Dependencies = (from tsk in dependencies select new TaskInList() { Id = tsk.Id, Description = tsk.Description, Alias = tsk.Alias, Status = tsk.Status }).ToList(),
        };

    }
    /// <summary>
    /// //Task crud functions
    /// </summary>
    /// <param name="s_dalTask"></param>
    static void createTask(BlApi.ITask? s_dalTask)
    {
        s_dalTask?.Create(getTask(false));
    }
    /// <summary>
    /// //Task crud functions
    /// </summary>
    /// <param name="s_dalTask"></param>
    static void updateTask(BlApi.ITask? s_dalTask)
    {
        s_dalTask?.Update(getTask(true));
    }
    /// <summary>
    ///     /// //Task crud functions
    /// </summary>
    /// <param name="s_dalTask"></param>
    static void deleteTask(BlApi.ITask? s_dalTask)
    {
        Console.WriteLine("Enter Id:");
        int id = int.Parse(Console.ReadLine()!);
        s_dalTask?.Delete(id);
    }
    /// <summary>
    /// //Task crud functions
    /// </summary>
    /// <param name="s_dalTask"></param>
    static void printAllTasks(BlApi.ITask? s_dalTask)
    {
        foreach (var task in s_bl!.Task.ReadAllTask())
        { Console.WriteLine(task); }
    }
    /// <summary>
    /// //Task crud functions
    /// </summary>
    /// <param name="s_dalTask"></param>
    static void searchTask(BlApi.ITask? s_dalTask)
    {
        Console.WriteLine("Enter Id:");
        int id = int.Parse(Console.ReadLine()!);
        BO.Task? task = s_dalTask?.Read(id);
        Console.WriteLine(task);
    }

    //############################
    //dates and sceduale functions
    //############################
    /// <summary>
    /// //Schedule
    /// </summary>
    static void Schedule()
    {

        while (s_bl!.Task.ReadAllTask(item => item.Status == BO.Status.Unscheduled).ToList().Any())
        {
            foreach (var task in s_bl!.Task.ReadAllTask(item => item.Status == BO.Status.Unscheduled))
            {
                try
                {
                    Console.WriteLine($"Enter the scehdule date of task {task.Id}");
                    DateTime scheduleDate = DateTime.Parse(Console.ReadLine()!);
                    //DateTime scheduleDate = DateTime.Now;
                    s_bl.Task.UpdateDates(task.Id, scheduleDate);
                }
                catch (BO.BlDateProblemException ex) { Console.WriteLine(ex.Message); }
            }
        }

    }

    /// <summary>
    /// //User menu
    /// </summary>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    static void assigntoUser()
    {
        Console.WriteLine("Enter the id of the task");
        int id = int.Parse(Console.ReadLine()!);
        if (s_bl?.Task.Read(id) == null) throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        Console.WriteLine("Enter the id of the user");
        int userId = int.Parse(Console.ReadLine()!);
        if (s_bl?.User.Read(userId) == null) throw new BO.BlDoesNotExistException($"User with ID={userId} does Not exist");
        BO.Task task = s_bl?.Task.Read(id)!;
        if (task.User != null)
            throw new BO.BlAlreadyExistException($"Task with ID={id} is already assigned to a user");
        BO.UserInTask userInTask = new BO.UserInTask() { Id = userId, Name = s_bl?.User.Read(userId)!.Name };
        task.User = userInTask;

        BO.User user = s_bl?.User.Read(userId)!;
        BO.TaskInUser taskInUser = new BO.TaskInUser() { Id = id, Alias = s_bl?.Task.Read(id)!.Alias };
        user.Tasks.Add(taskInUser);
        s_bl?.Task.Update(task);
        s_bl?.User.Update(user);

    }
    /// <summary>
    /// //User menu
    /// </summary>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    static void updatetaskproject2()
    {
        int id = int.Parse(GetString("Enter the id of the task"));
        if (s_bl?.Task.Read(id) == null) throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        string? des = GetString("Write Description of task: ");
        string? alias = GetString("Write Alias of task: ");
        int cost = int.Parse(GetString("Enter Cost of task: "));
        string? del = GetString("Write Deliverables: ");
        string? rem = GetString("Write Remarks");
        BO.Task task = s_bl?.Task.Read(id)!;
        if (des != null)
            task.Description = des;
        if (alias != null)
            task.Alias = alias;
        if (cost != 0)
            task.Cost = cost;
        if (del != null)
            task.Deliverables = del;
        if (rem != null)
            task.Remarks = rem;
        s_bl?.Task.Update(task);
    }

    /// <summary>
    /// //Main menu
    /// </summary>
    static void menumainproject2()
    {
        Console.WriteLine("0: exit");
        Console.WriteLine("1: user menu");
        Console.WriteLine("2: task menu");
        Console.WriteLine("3: assign a task to user");
    }
    /// <summary>
    /// //User menu
    /// </summary>
    static void usermenu()
    {
        Console.WriteLine("0: exit");
        Console.WriteLine("1: delete a user");
        Console.WriteLine("2: create a user");
        Console.WriteLine("3: update a user");
        Console.WriteLine("4: print all the users");
        Console.WriteLine("5: search for a user");

    }
    /// <summary>
    /// //Task menu
    /// </summary>
    static void taskmenu()
    {
        Console.WriteLine("0: exit");
        Console.WriteLine("1: update a task");
        Console.WriteLine("2: print all the tasks");
        Console.WriteLine("3: search for a task");
        Console.WriteLine("4: promote status task");
    }
    /// <summary>
    /// Stage 2 of the project
    ///
    /// </summary>
    static void Stage2project()
    {
        menumainproject2();
        int choice = int.Parse(Console.ReadLine()!);
        int choice2;
        while (choice != 0)
        {
            switch (choice)
            {
                case 0: break;
                case 1:
                    usermenu();
                    choice2 = int.Parse(Console.ReadLine()!);
                    while (choice2 != 0)
                    {
                        try
                        {
                            switch (choice2)
                            {
                                case 0: break;
                                case 1: deleteUser(s_bl.User); break;
                                case 2: createUser(s_bl.User); break;
                                case 3: updateUser(s_bl.User); break;
                                case 4: printAllUsers(s_bl.User); break;
                                case 5: searchUser(s_bl.User); break;
                                default:
                                    Console.WriteLine("Incorrect input, plese press a number from 0 to 5");
                                    break;
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        usermenu();
                        choice2 = int.Parse(Console.ReadLine()!);

                    }
                    break;
                case 2:
                    taskmenu();
                    choice2 = int.Parse(Console.ReadLine()!);

                    while (choice2 != 0)
                    {
                        try
                        {
                            switch (choice2)
                            {
                                case 0: break;
                                case 1: updatetaskproject2(); break;
                                case 2: printAllTasks(s_bl.Task); break;
                                case 3: searchTask(s_bl.Task); break;

                                case 4:
                                    Console.WriteLine("Enter the id of the task");
                                    int id = int.Parse(Console.ReadLine()!);
                                    s_bl.Task.promoteStatusTask(id); break;
                                default:
                                    Console.WriteLine("Incorrect input, plese press a number from 0 to 5");
                                    break;
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        taskmenu();
                        choice2 = int.Parse(Console.ReadLine()!);

                    }
                    break;
                case 3:
                    try { assigntoUser(); } catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
                default:
                    Console.WriteLine("Incorrect input, plese press a number from 0 to 2");
                    break;
            }
            menumainproject2();
            choice = int.Parse(Console.ReadLine()!);
        }

    }
    /// <summary>
    /// this is the main function of the program
    /// </summary>
    /// <param name="args"></param>
    /// <exception cref="FormatException"></exception>
    static void Main(string[] args)
    {
        Console.WriteLine("\t~ WedPlan ~\t\n\n");
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y" || ans == "y")
        {
            s_bl.Clock.resetTimeProject();
            DalTest.Initialization.Do();
        }
        if (s_bl.Clock.GetStatusProject() == BO.StatusProject.UnStarted)
        {
            Console.WriteLine("The planing stage");//stage 1 and 2 of the project
            Stage1project();
        }
        if (s_bl.Clock.GetStatusProject() == BO.StatusProject.InProgress)
        {
            Console.WriteLine("The project is in progress");//stage 3 of the project
            Stage2project();

        }
        if (s_bl.Clock.GetStatusProject() == BO.StatusProject.Done)
        {
            Console.WriteLine("The project is finished");
        }


    }
}
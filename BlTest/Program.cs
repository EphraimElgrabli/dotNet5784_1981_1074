using Dal;
using BlApi;
using Microsoft.VisualBasic.FileIO;
using BlImplementation;
using BO;
using DO;

namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();

    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
            DalTest.Initialization.Do();

        int mainMenu, subMenu;

        mainMenu = MainMenu();
        while(mainMenu != 0)
        {
            try
            {
                switch(mainMenu)
                {
                    case 0:
                        break;
                    case 1:
                        subMenu = UserMenu();
                        while (subMenu != 1)
                        {
                            switch (subMenu)
                            {
                                case 1: break;
                                case 2: deleteUser(s_bl.User); break;
                                case 3: createUser(s_bl.User); break;
                                case 4: updateUser(s_bl.User); break;
                                case 5: printAllUsers(s_bl.User); break;
                                case 6: searchUser(s_bl.User); break;
                                default:
                                    Console.WriteLine("Incorrect input, plese press a number from 1 to 6");
                                    break;
                            }
                            subMenu = UserMenu();
                        }
                        break;
                    case 2:
                        subMenu = UserMenu();
                        while (subMenu != 1)
                        {
                            switch (subMenu)
                            {
                                case 1: break;
                                case 2: deleteTask(s_bl.Task); break;
                                case 3: createTask(s_bl.Task); break;
                                case 4: updateTask(s_bl.Task); break;
                                case 5: printAllTasks(s_bl.Task); break;
                                case 6: searchTask(s_bl.Task); break;
                                default:
                                    Console.WriteLine("Incorrect input, plese press a number from 1 to 6");
                                    break;
                            }
                            subMenu = UserMenu();
                        }
                        break;
                    case 3:
                        Console.WriteLine("Now you are about to set dates for tasks");
                        Schedule();
                        UpdatedMainMenu();
                        break;
                    default:
                        Console.WriteLine("Incorrect input, plese press a number from 1 to 4");
                        break;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        //  ########################
        //  Printing functions
        //  ########################
        //  Print the main menu
        int MainMenu()
        {
            Console.WriteLine("To exit, press 0");
            Console.WriteLine("To the User menu, press 1");
            Console.WriteLine("To the Task menu, press 2");
            Console.WriteLine("To update the dates, press 3");
            int choice = int.Parse(Console.ReadLine()!);
            return choice;
        }
        //  Print the main menu that will appear after dates updating
        int UpdatedMainMenu()
        {
            Console.WriteLine("To exit, press 0");
            Console.WriteLine("To the updated User menu, press 1");
            Console.WriteLine("To the updated Task menu, press 2");
            int choice = int.Parse(Console.ReadLine()!);
            return choice;
        }
        //  Print the menu of the Engineer
        int UserMenu()
        {
            Console.WriteLine("To exit this current menu , press 1");
            Console.WriteLine("To delete a User, press 2");
            Console.WriteLine("To create a User, press 3");
            Console.WriteLine("To update a User, press 4");
            Console.WriteLine("To print all the Users, press 5");
            Console.WriteLine("To search for a User, press 6");
            int choice = int.Parse(Console.ReadLine()!);
            return choice;
        }
        //  Print the menu of the Engineer
        int TaskMenu()
        {
            Console.WriteLine("To exit this current menu, press 1");
            Console.WriteLine("To delete a Task, press 2");
            Console.WriteLine("To create a Task, press 3");
            Console.WriteLine("To update a Task, press 4");
            Console.WriteLine("To print all the Tasks, press 5");
            Console.WriteLine("To search for an Task, press 6");
            int choice = int.Parse(Console.ReadLine()!);
            return choice;
        }
        int UpdatedTaskMenu()
        {
            Console.WriteLine("TTo exit this current menu, press 1");
            Console.WriteLine("To delete a Task, press 2");
            Console.WriteLine("To print all the Tasks, enter 3");
            Console.WriteLine("To search for a Task, enter 4");
            int choice = int.Parse(Console.ReadLine()!);
            return choice;
        }

        //###############
        //crud functions
        //###############

        //Useful Function
        string GetString(string s)
        {
            Console.WriteLine(s);
            return Console.ReadLine()!;
        }
        //User crud functions
        BO.User getUser()
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

            //getting from the user input about the task
            Console.WriteLine("Enter the id of the task:");
            int taskId = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter alias of task:");
            string taskAlias = Console.ReadLine()!;

            // Create a new Engineer object with the updated details
            return new BO.User()
            {
                Id = id,
                Name = name,
                Email = email,
                PhoneNumber = phone,
                Level = level,
                Tasks = new BO.TaskInUser() { Id = taskId, Alias = taskAlias }
            };
        }
        void createUser(BlApi.IUser? s_dalUser)
        {
            s_dalUser?.Create(getUser());
        }
        void updateUser(BlApi.IUser? s_dalUser)
        {
            s_dalUser?.Update(getUser());
        }
        void deleteUser(BlApi.IUser? s_dalUser)
        {
            Console.WriteLine("Enter Id:");
            int id = int.Parse(Console.ReadLine()!);
            s_dalUser?.Delete(id);
        }
        void printUser(BO.User user)
        {
            Console.WriteLine($"User ID: {user.Id}");
            Console.WriteLine($"User name: {user.Name}");
            Console.WriteLine($"User email: {user.Email}");
            Console.WriteLine($"User phone number: {user.PhoneNumber}");
            Console.WriteLine($"User level: {user.Level}");
            if (user.Tasks is not null)
            {
                Console.WriteLine("Engineer task id: " + user.Tasks!.Id);
                Console.WriteLine("Engineer task alias: " + user.Tasks.Alias + '\n');
            }
            else
            {
                Console.WriteLine("Engineer task id: not set");
                Console.WriteLine("Engineer task alias: not set\n");
            }
        }
        void printAllUsers(BlApi.IUser? s_dalUser)
        {
            List<BO.User> users = new List<BO.User>(s_dalUser?.ReadAll()!);
            foreach (BO.User user in users)
            {
                printUser(user);
            }
        }
        void searchUser(BlApi.IUser? s_dalUser)
        {
            Console.WriteLine("Enter Id:");
            int id = int.Parse(Console.ReadLine()!);
            BO.User? user = s_dalUser?.Read(id);
            if (user == null)
            {
                Console.WriteLine("The user Dosent Exist In The system");
            }
            else
            {
                printUser(user);
            }
        }

        //Task crud functions
        BO.Task getTask(bool isUpdate)
        {
            int id;
            if (isUpdate)
            {
                Console.WriteLine("Enter Id: ");
                id = int.Parse(Console.ReadLine()!);
            }
            else
            {
                id = 0;
            }

            // Prompt the user to enter updated details for the Task
            string description = GetString("Enter description:");
            string alias = GetString("Enter alias:");
            DateTime createAtDate = DateTime.Now;
            Console.WriteLine("Enter is milestone:");
            bool milestone = bool.Parse(Console.ReadLine()!); // not sure yet if we will do the milestone part
            Console.WriteLine("Enter Engineer experience:");
            BO.UserLevel level = Enum.Parse<BO.UserLevel>(Console.ReadLine()!);
            Console.WriteLine("Enter the start date:");
            DateTime startDate = DateTime.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter the schedule date:");
            DateTime scheduleDate = DateTime.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter the deadline date:");
            DateTime deadlineDate = DateTime.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter the complete date:");
            DateTime completeDate = DateTime.Parse(Console.ReadLine()!);
            string deliverables = GetString("Enter deliverables:");
            string remarks = GetString("Enter remarks:");
            Console.WriteLine("Enter Engineer Id:");
            int userId = int.Parse(Console.ReadLine()!);
            int menu;
            List<TaskInList> dependencies = new List<TaskInList>();

            do
            {//do change
                Console.WriteLine("Enter the Id of the previous task:");
                int previousTaskId = int.Parse(Console.ReadLine()!);
                string taskDescription = ("Enter the description of the dependency task:"); ;
                string taskAlias = GetString("Enter the alias of the dependency task:");
                Console.WriteLine("Enter the status of the dependency task:");
                BO.Status status = Enum.Parse<BO.Status>(Console.ReadLine()!);
                Console.WriteLine("would you like to add another dependency?\n if so press 1");
                TaskInList taskInList = new TaskInList() { Id = previousTaskId, Description = taskDescription, Alias = taskDescription, Status = status };
                dependencies.Add(taskInList);
                menu = int.Parse(Console.ReadLine()!);
            } while (menu == 1);
            // Create a new Task object with the updated details
            return new BO.Task()
            {
                Id = id,
                Alias = alias,
                Description = description,
                CreatedAtDate = createAtDate,
                Complexity = level,
                StartDate = startDate,
                ScheduledDate = scheduleDate,
                DeadlineDate = deadlineDate,
                CompleteDate = completeDate,
                Deliverables = deliverables,
                Dependencies = (from tsk in dependencies select new TaskInList() { Id = tsk.Id, Description = tsk.Description, Alias = tsk.Alias, Status = tsk.Status }),
            };
        }
        void createTask(BlApi.ITask? s_dalTask)
        {
            s_dalTask?.Create(getTask(false));
        }
        void updateTask(BlApi.ITask? s_dalTask)
        {
            s_dalTask?.Update(getTask(true));
        }
        void deleteTask(BlApi.ITask? s_dalTask)
        {
            Console.WriteLine("Enter Id:");
            int id = int.Parse(Console.ReadLine()!);
            s_dalTask?.Delete(id);
        }
        void printTask(BO.Task task)
        {
            Console.WriteLine($"Task ID: {task.Id}");
            Console.WriteLine($"Task description: {task.Description}");
            Console.WriteLine($"Task alias: {task.Alias}");
            Console.WriteLine($"Task creation date: {task.CreatedAtDate}");
            Console.WriteLine($"Task start date: {task.StartDate}");
            Console.WriteLine($"Task scheduled date: {task.ScheduledDate}");
            Console.WriteLine($"Task deadline date: {task.DeadlineDate}");
            Console.WriteLine($"Task complete date: {task.CompleteDate}");
            Console.WriteLine($"Task level: {task.Complexity}");
            Console.WriteLine("Task Deliverables: " + task.Deliverables);
            Console.WriteLine("Task Remarks: " + task.Remarks);

            foreach (var dependency in task.Dependencies)
            {
                Console.WriteLine("Task dependencie Id: " + dependency.Id);
                Console.WriteLine("Task dependencie Alias: " + dependency.Alias);
                Console.WriteLine("Task dependencie Description: " + dependency.Description);
                Console.WriteLine("Task dependencie Status: " + dependency.Status + '\n');
            }
            Console.WriteLine("");
        }
        void printAllTasks(BlApi.ITask? s_dalTask)
        {
            List<BO.Task> tasks = new List<BO.Task>(s_dalTask?.ReadAll(null)!);
            foreach (BO.Task task in tasks)
            {
                printTask(task);
            }
        }
        void searchTask(BlApi.ITask? s_dalTask)
        {
            Console.WriteLine("Enter Id:");
            int id = int.Parse(Console.ReadLine()!);
            BO.Task? task = s_dalTask?.Read(id);

            // Check if the Task was found
            if (task == null)
            {
                Console.WriteLine("This Task doesn't exist.");
            }
            else
            {
                printTask(task);
            }
        }

        //############################
        //dates and sceduale functions
        //############################

        void Schedule()
        {
            //לבדוק את כל הרשימות לתת לכולם זמן תחילת פרוויקט
            //לבדוק שכולם אחרי התאריך הקודם שלהם
            
        }
    }
}
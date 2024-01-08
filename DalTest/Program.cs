namespace DalTest;
using Dal;
using DalApi;
using DO;
using System;

internal class Program
{
    private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    private static IUser? s_dalUser = new UserImplementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    static int funcentity()
    {
        Console.WriteLine("\nCheck entity\n");
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0: exit");
        Console.WriteLine("1: Dependency");
        Console.WriteLine("2: Task");
        Console.WriteLine("3: User");
        int entity = int.Parse(Console.ReadLine()!);
        return entity;
    }
    static int functask(string s)
    {
        Console.WriteLine("\nchoose operation\n");
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0: exit");
        Console.WriteLine("1: Create new " ,s);
        Console.WriteLine("2: Read ",s);
        Console.WriteLine("3: ReadAll ",s);
        Console.WriteLine("4: Update ",s);
        Console.WriteLine("5: Delete ",s);
        int entity = int.Parse(Console.ReadLine()!);
        return entity;
    }
    public static string GetString(string s)
    {
        Console.WriteLine(s);
        return Console.ReadLine();
    }

   
    private static void User()
    {
        int numtask = functask("User");
        switch (numtask)
        {
           case 0: return;
           case 1:
                Console.WriteLine("\nPlease Enter Details\n");
                int Id = int.Parse(GetString("4 Last Digit Of Id: "));
                string Email= GetString("Email: ");
                string PhoneNumber = GetString("PhoneNumber: ");
                string Name= GetString("Name: ");
                DO.UserLevel Level = (DO.UserLevel)Int16.Parse(GetString("0: supportes\r\n  1: closeFriends\r\n  2: bride\r\n  3: groom\r\n  4: producer "));
                User newUser = new(Id, Email, PhoneNumber.ToString(),Name,Level);
                s_dalUser!.Create(newUser);
                break;
           case 2:

        }
    }

    private static void Task()
    {
        int numtask = functask("Task");
        try
        {
            switch (numtask)
            {
                case 0: return;
                case 1:
                    Console.WriteLine("\nPlease Enter Details\n");
                    string alias = GetString("Alias: ");
                    string description = GetString("Description: ");
                    DateTime createdDate = DateTime.Now;
                    bool isMilestone = false;
                    DO.UserLevel level = (DO.UserLevel)Int16.Parse(GetString("0: supportes\r\n  1: closeFriends\r\n  2: bride\r\n  3: groom\r\n  4: producer "));
                    DateTime startDate = Convert.ToDateTime(GetString("Start Date (Formatted: 1/1/0001: "));
                    DateTime scheduledDate = Convert.ToDateTime(GetString("Scedualed Date (Formatted: 1/1/0001: "));
                    DateTime deadlineDate = Convert.ToDateTime(GetString("Deadline Date (Formatted: 1/1/0001: "));
                    DateTime completeDate = Convert.ToDateTime(GetString("Complete Date (Formatted: 1/1/0001: "));
                    string deliverables = GetString("Deliverables: ");
                    string remarks = GetString("Remarks: ");
                    Task newTask = new(0,alias, description, createdDate, isMilestone, level, startDate, scheduledDate, deadlineDate, completeDate,deliverables,remarks,0);
                    s_dalTask!.Create(newTask);
                    break;
                case 2:

            }
        }
    }
    /// <summary>
    /// int Id,
    //string Alias, v 
    //string Description, v
    //DateTime CreatedAtDate, v
    //bool IsMilestone, v
    //DO.UserLevel Copmlexity,
    //DateTime StartDate,
    //DateTime ScheduledDate,
    //DateTime DeadlineDate,
    //DateTime CompleteDate,
    //string Deliverables,
    //string Remarks,
    //int EngineerId  //conect to engineerid
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        int entity = funcentity();
        User? temp = s_dalUser!.Read(entity);
        entity=functask();

        Initialization.Do(s_dalDependency,s_dalUser, s_dalTask);
        
      

    }
}
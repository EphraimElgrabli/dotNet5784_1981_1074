﻿namespace DalTest;
using DalApi;
using DO;
using System;


internal class Program
{


    // Function to interact with different entities (Dependency, Task, User)
    static int funcentity()
    {
        Console.WriteLine("\nCheck entity\n");
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0: exit");
        Console.WriteLine("1: Dependency");
        Console.WriteLine("2: Task");
        Console.WriteLine("3: User");
        int entity = int.Parse(Console.ReadLine()!);
        do
        {
            try
        {
            
                switch (entity)
                {
                    case 0: break;
                    case 1: Dependency(); break;
                    case 2: Task(); break;
                    case 3: User(); break;
                    default: Console.WriteLine("Invaild output"); break;
                }
                Console.WriteLine("\nCheck entity\n");
                Console.WriteLine("Choose one of the following:");
                Console.WriteLine("0: exit");
                Console.WriteLine("1: Dependency");
                Console.WriteLine("2: Task");
                Console.WriteLine("3: User");
                entity = int.Parse(Console.ReadLine()!);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        } while (entity != 0);
        return entity;
    }

    /// <summary>
    /// Function to interact with different task operations (Create, Read, Update, Delete)
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    static int functask(string s)
    {

        Console.WriteLine("\nchoose operation\n");
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0: exit");
        Console.WriteLine("1: Create new ", s);
        Console.WriteLine("2: Read ", s);
        Console.WriteLine("3: ReadAll ", s);
        Console.WriteLine("4: Update ", s);
        Console.WriteLine("5: Delete ", s);
        int entity = int.Parse(Console.ReadLine()!);
        return entity;
    }

    /// <summary>
    /// Function to get user input as a string
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string GetString(string s)
    {
        Console.WriteLine(s);
        return Console.ReadLine();
    }

    /// <summary>
    /// Function to handle user-related operations
    /// </summary>
    private static void User()
    {
        int numtask = functask("User");
        do
        {
            switch (numtask)
            {
                case 0: break;

                case 1:
                    Console.WriteLine("\nPlease Enter Details\n");
                    int Id = int.Parse(GetString("Digit Of Id: "));
                    string Email = GetString("Email: ");
                    string PhoneNumber = GetString("PhoneNumber: ");
                    string Name = GetString("Name: ");
                    DO.UserLevel Level = (DO.UserLevel)Int16.Parse(GetString("\r0: Friends\r\n1: Staff\r\n2: FamilyMember\r\n3: FamilyMemberWithRoles\r\n4: Producer"));
                    User newUser = new(Id, Email, PhoneNumber,"12345", Name, Level);
                    Console.WriteLine(s_dal!.User.Create(newUser));
                    break;

                case 2:
                    Id = int.Parse(GetString("Id: "));
                    User? temPrint = s_dal!.User.Read(Id);
                    if (temPrint != null)
                    {
                        Console.WriteLine(temPrint);
                    }
                    break;

                case 3:
                    List<User> temPrintAll = s_dal.User!.ReadAll().ToList();
                    foreach (User p in temPrintAll)
                    {
                        Console.WriteLine(p);
                    }
                    break;

                case 4:
                    Id = int.Parse(GetString("Id: "));
                    User? temp = s_dal!.User.Read(Id);
                    Console.WriteLine(temp);
                    Console.WriteLine("\nPlease Enter Details\n");
                    Email = GetString("Email: ");
                    PhoneNumber = GetString("PhoneNumber: ");
                    Name = GetString("Name: ");
                    Level = (DO.UserLevel)Int16.Parse(GetString("\r0: Friends\r\n1: Staff\r\n2: FamilyMember\r\n3: FamilyMemberWithRoles\r\n4: Producer"));

                    if (Email == "")
                        Email = temp.Email;
                    if (PhoneNumber == "")
                        PhoneNumber = temp.PhoneNumber;
                    if (Name == "")
                        Name = temp.Name;
                    if ((int)(Level) < 0 || (int)(Level) > 5)
                        Level = temp.Level;

                    User Helpuscheck = new(Id, Email, PhoneNumber,"12345", Name, Level);
                    s_dal!.User.Update(Helpuscheck);
                    break;

                case 5:
                    Id = int.Parse(GetString("Id: "));
                    s_dal!.User.Delete(Id);
                    break;
            }
            numtask = functask("User");
        } while (numtask != 0);
    }

    /// <summary>
    /// Function to handle dependency-related operations
    /// </summary>
    private static void Dependency()
    {
        int numtask = functask("Dependency");
        do
        {
            switch (numtask)
            {
                case 0: return;

                case 1:
                    int dependentTask = int.Parse(GetString("Dependent Task: "));
                    int dependsOnTask = int.Parse(GetString("Depends On Task: "));
                    Dependency newDepn = new(0,dependentTask, dependsOnTask);
                    Console.WriteLine(s_dal!.Dependency.Create(newDepn));
                    break;

                case 2:
                    int Id = int.Parse(GetString("Id: "));
                    Dependency? temPrint = s_dal!.Dependency.Read(Id);
                    if (temPrint != null)
                    {
                        Console.WriteLine(temPrint);
                    }
                    break;

                case 3:
                    List<Dependency?> temPrintAll = s_dal!.Dependency.ReadAll().ToList();
                    foreach (Dependency? dep in temPrintAll)
                    {
                        Console.WriteLine(dep);
                    }
                    break;

                case 4:
                    Id = int.Parse(GetString("Id: "));
                    Dependency? temp = s_dal!.Dependency.Read(Id);
                    Console.WriteLine(temp);
                    Console.WriteLine("\nPlease Enter Details\n");
                    dependentTask = Int16.Parse(GetString("Dependent Task: "));
                    dependsOnTask = Int16.Parse(GetString("Depends On Task: "));

                    if (dependentTask == '\n')
                        dependentTask = temp.DependentTask;
                    if (dependsOnTask == '\n')
                        dependsOnTask = temp.DependsOnTask;

                    Dependency Helpuscheck = new(Id, dependentTask, dependsOnTask);
                    s_dal!.Dependency.Update(Helpuscheck);
                    break;
                case 5:
                    Id = int.Parse(GetString("Id: "));
                    s_dal!.Dependency.Delete(Id);
                    break;
            }
            numtask = functask("Dependency");
        } while (numtask != 0);
    }

    /// <summary>
    /// Function to handle task-related operations
    /// </summary>
    private static void Task()
    {
        int numtask = functask("Task");
        do
        {
            switch (numtask)
            {
                case 0: return;
                case 1:
                    Console.WriteLine("\nPlease Enter Details\n");
                    string alias = GetString("Alias: ");
                    string description = GetString("Description: ");
                    DateTime createdDate = DateTime.Now;
                    int cost = int.Parse(GetString("Cost: "));
                    bool isMilestone = false;
                    DO.UserLevel? level = (DO.UserLevel)Int16.Parse(GetString("\r0: Friends\r\n1: Staff\r\n2: FamilyMember\r\n3: FamilyMemberWithRoles\r\n4: Producer")); 
                    DateTime? startDate =  Convert.ToDateTime(GetString("Start Date (Formatted: 1/1/0001): "));
                    DateTime? scheduledDate = Convert.ToDateTime(GetString("Scedualed Date (Formatted: 1/1/0001): "));
                    DateTime? deadlineDate = Convert.ToDateTime(GetString("Deadline Date (Formatted: 1/1/0001): "));
                    DateTime? completeDate = Convert.ToDateTime(DateTime.Parse(GetString("Complete Date (Formatted: 1/1/0001): ")));
                    string? deliverables = GetString("Deliverables: ");
                    string? remarks = GetString("Remarks: ");
                    Task newTask = new(0, alias, description,cost,0,0,0, createdDate, isMilestone, startDate, scheduledDate, deadlineDate, completeDate, deliverables, remarks, 0, level);
                    s_dal!.Task.Create(newTask);
                    break;
                case 2:
                    int Id = int.Parse(GetString("Id: "));
                    Task? temPrint = s_dal!.Task.Read(Id);
                    if (temPrint != null)
                    {
                        Console.WriteLine(temPrint);
                    }
                    break;
                case 3:
                    List<Task> temPrintAll = s_dal!.Task.ReadAll().ToList();
                    foreach (Task task in temPrintAll)
                    {
                        Console.WriteLine(task);
                    }
                    break;
                case 4:
                    Id = int.Parse(GetString("Id: "));
                    Task? temp = s_dal!.Task.Read(Id);
                    Console.WriteLine(temp);
                    cost = int.Parse(GetString("Cost: "));
                    alias = GetString("Alias: ");
                    description = GetString("Description: ");
                    isMilestone = bool.Parse(GetString("is it a milestone: "));
                    level = (DO.UserLevel)Int16.Parse(GetString("\r0: Friends\r\n1: Staff\r\n2: FamilyMember\r\n3: FamilyMemberWithRoles\r\n4: Producer"));
                    startDate = Convert.ToDateTime(GetString("Start Date (Formatted: 1/1/0001: "));
                    scheduledDate = Convert.ToDateTime(GetString("Scedualed Date (Formatted: 1/1/0001: "));
                    deadlineDate = Convert.ToDateTime(GetString("Deadline Date (Formatted: 1/1/0001: "));
                    completeDate = Convert.ToDateTime(GetString("Complete Date (Formatted: 1/1/0001: "));
                    deliverables = GetString("Deliverables: ");
                    remarks = GetString("Remarks: ");

                    DateTime dt = new DateTime();
                    if (alias == "")
                        alias = temp.Alias;
                    if (description == "")
                        description = temp.Description;
                    if ((int)(level) < 0 || (int)(level) > 5)
                        level = temp.Complexity;
                    if(cost==temp.cost)
                        cost = temp.cost;
                    if (scheduledDate == dt)
                        scheduledDate = temp.ScheduledDate;
                    if (deadlineDate == dt)
                        deadlineDate = temp.DeadlineDate;
                    if (completeDate == dt)
                        completeDate = temp.CompleteDate;
                    if (deliverables == "")
                        deliverables = temp.Deliverables;
                    if (remarks == "")
                        remarks = temp.Remarks;
                    Task Helpuscheck = new(Id, alias, description,cost,0,0,0, temp.CreatedAtDate, isMilestone, startDate, scheduledDate, deadlineDate, completeDate, deliverables, remarks, 0, level);
                    s_dal!.Task.Update(Helpuscheck);
                    break;

                case 5:
                    Id = int.Parse(GetString("Id: "));
                    s_dal!.Task.Delete(Id);
                    break;
            }
            numtask = functask("Task");
        } while (numtask != 0);

    }
    //static readonly IDal s_dal = new DalList(); //stage 2
    //static readonly IDal s_dal = new DalXml(); //stage 3
    static readonly IDal s_dal = Factory.Get; //stage 4
    static void Main(string[] args)
    {
        // Main entry point of the program
        
            Console.WriteLine("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y" || ans == "y")
                Initialization.Do(); //stage 2          
            funcentity(); // Performing operations on different entities
        
        
    }
}
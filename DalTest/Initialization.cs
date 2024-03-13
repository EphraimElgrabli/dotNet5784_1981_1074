namespace DalTest;

using System.Data.Common;
using DalApi;
using DO;
using System.Reflection;
using System.Security.Cryptography;
using System;

public static class Initialization
{
    // Data access layer instances for Dependency, User, and Task
    private static IDal? s_dal;
    private static readonly Random s_rand=new();

    /// <summary>
    /// Function to create random task
    /// </summary>
    private static void createTask()
    {
        // Array of task aliases and descriptions
        string[] taskArr = new string[] {
            "overallBudget",
            "weddingDate",
            "guestList",
            "venue",
            "weddingPlannerHired",
            "vendors",
            "themeStyle",
            "weddingAttire",
            "weddingWebsite",
            "saveTheDatesSent",
            "invitationsSent",
            "ceremonyDetails",
            "receptionDetails",
            "necessaryDocumentsObtained",
            "dayOfTimeline",
            "transportationArranged",
            "rehearsalDinnerDetails",
            "weddingRings",
            "accommodations",
            "hairMakeupTrialsScheduled",
            "honeymoonDetails",
            "giftRegistry",
            "preWeddingCelebrations",
            "finalDetailsConfirmed",
            "seatingChart",
            "welcomeBagsAssembled",
            "finalGuestCountConfirmed",
            "rehearsalConducted",
            "weddingDayExecuted",
            "postWeddingTasksCompleted",
            "mikva",
            "hina"
        };

        foreach (var task in taskArr)
        {
            // Generate random values for task creation
            string? alias = task;
            string? description = task;
            DateTime createdDate = DateTime.Now;
            int cost = s_rand.Next(1000, 10000);
            bool isMilestone = (s_rand.Next(0, 1000) % 2) == 0 ? true : false;
            int userLvl = s_rand.Next(0, 4);
            DateTime end = new DateTime(2025, 1, 1);
            int range = (end - DateTime.Today).Days;
            DateTime scedualed = end.AddDays(s_rand.Next(range));
            DateTime Startdate = scedualed.AddDays(-2);
            DateTime deadDate = scedualed.AddDays(+1);
            DateTime completeDate = scedualed;
            string Delivarbles = "Hurry Up, the groom is going to regrat from getting married";
            string Remarks = "Hurry Up, the groom is going to regrat from getting married";
            int engId = s_rand.Next(1000, 1020);

            // Create a new Task object and add it to the data access layer
            Task newTask = new(0, alias, description,cost, createdDate, isMilestone, Startdate, scedualed, deadDate, completeDate, Delivarbles, Remarks, engId, (DO.UserLevel)userLvl);
            s_dal!.Task.Create(newTask);
        }
    }

    /// <summary>
    /// Function to create random users
    /// </summary>
    private static void createUser()
    {
        // Array of user names
        string[] users = new string[] {
            "bigMama",
            "bigPapa",
            "Kala",
            "Hatan",
            "yoram",
            "shimoshon"
        };

        foreach (string user in users) {
            // Generate random values for user creation
            int id =s_rand.Next(200000000, 400000000); 
            int userLvl = s_rand.Next(0, 4);
            int phoneNumber = s_rand.Next(97200, 97299);
            string Password = "123456";
            string nate = user + "@gmail.com";

            // Create a new User object and add it to the data access layer
            User newUser = new(id, nate, phoneNumber.ToString(),Password, user, (DO.UserLevel)userLvl);
            s_dal!.User.Create(newUser);
        }
        User newUser1 = new(111111, "Admin@gmail.com","0584615194","Pro12345","Admin", DO.UserLevel.Producer);
        s_dal!.User.Create(newUser1);
    }

    /// <summary>
    /// Function to create dependencies between tasks
    /// </summary>
    private static void createDependency()
    {
        // Array of task dependencies
        (int, int)[] dependency = new (int, int)[]
        {
            // List of task dependencies
            (1001,1000),(1002,1001),(1003,1000),(1003,1001),
            (1004,1001),(1004,1000),(1004,1003),(1005,1000),
            (1005,1003),(1005,1004),(1006,1000),(1006,1003),
            (1007,1001),(1008,1000),(1008,1002),(1009,1000),
            (1010,1002),(1011,1000),(1011,1002),(1012,1001),
            (1012,1002),(1012,1009),(1013,1000),(1013,1003),
            (1013,1011),(1014,1001),(1014,1003),(1014,1012),
            (1015,1001),(1015,1011),(1015,1012),(1016,1000),
            (1016,1003),(1016,1011),(1017,1000),(1017,1003),
            (1017,1011),(1018,1001),(1018,1015),(1019,1000),
            (1019,1003),(1019,1011),(1020,1003),(1020,1015),
            (1021,1002),(1021,1012),(1021,1018),(1022,1000),
            (1022,1012),(1022,1018),(1023,1000),(1023,1011),
            (1023,1018),(1024,1002),(1024,1018),(1025,1003),
            (1025,1018),(1026,1000),(1026,1011),(1026,1018),
            (1027,1003),(1027,1018),(1028,1003),(1028,1018),
            (1029,1003),(1029,1020),(1030,1003),(1030,1025),
            (1031,1003),(1031,1015),(1032,1027),(1032,1028),
            (1032,1030),(1033,1000),(1034,1001)
        };
        foreach((int hey1,int hey2)  in dependency)
        {
            // Create a new Dependency object and add it to the data access layer
            Dependency temp = new(0, hey1, hey2);
            s_dal!.Dependency.Create(temp);
        }
    }

    /// <summary>
    /// Create a new Dependency object and add it to the data access layer
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <exception cref="NullReferenceException"></exception>
    public static void Do()
    {
        s_dal = Factory.Get; //stage 4
        // Ensure that data access layer instances are not null
        //s_dal=dal ?? throw new NullReferenceException("DAL can not be null!");

        // Create initial data for tasks, dependencies, and users
        s_dal.Task.DeleteAll();
        s_dal.Dependency.DeleteAll();
        s_dal.User.DeleteAll();

        createTask();
        createDependency();
        createUser();
    }
}

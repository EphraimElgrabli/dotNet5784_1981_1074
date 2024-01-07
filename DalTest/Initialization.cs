namespace DalTest;

using DalApi;
using DO;
using System.Reflection;
using System.Security.Cryptography;

public static class Initialization
{
    private static IDependency? s_dalDependency;//stage 1
    private static IUser? s_dalUser;//stage 1
    private static ITask? s_dalTask;//stage 1
    private static readonly Random s_rand=new();
    private static void createTask()
    {
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
            string alias = task;
            string description = task;

            DateTime createdDate = DateTime.Now;

            TimeSpan efforttime = TimeSpan.FromDays(2);

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

            Task newTask = new(0,alias, description, createdDate, efforttime, isMilestone, (DO.UserLevel)userLvl, Startdate, scedualed, deadDate, completeDate, Delivarbles, Remarks, engId);
            s_dalTask!.Create
        }
    }

    private static void createUser()
    {
        
        User Motherofgroom= new User();
        User Motherofbride= new User();
        User Fatherofgroom = new User();
        User Fatherofbride = new User();
        User Groom= new User();
        User Bride= new User();
    }
    private static void createDependency() { 
    
    }
}

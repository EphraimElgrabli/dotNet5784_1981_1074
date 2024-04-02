using DalApi;

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
    /// public void GanttTimestart()
    private static void createTask()
    {
        // Array of task aliases, descriptions, deliverables, and remarks
        string[,] taskArr = new string[,] {
        { "overallBudget", "Overall Budget", "Finalize the overall wedding budget", "Ensure the budget is realistic and comprehensive" },
        { "guestList", "Guest List", "Compile the complete guest list", "Ensure all important family members and friends are included" },
        { "venue", "Venue", "Book the wedding venue", "Consider capacity, location, and overall style" },
        { "weddingPlannerHired", "Wedding Planner Hired", "Hire a wedding planner", "Choose a planner who understands your vision and budget" },
        { "vendors", "Vendors", "Book all necessary vendors", "Include caterer, florist, photographer, videographer, and entertainment" },
        { "themeStyle", "Theme/Style", "Decide on the wedding theme and style", "Consider colors, decorations, and overall ambiance" },
        { "weddingAttire", "Wedding Attire", "Choose wedding attire for the couple and wedding party", "Consider style, color, and comfort" },
        { "weddingWebsite", "Wedding Website", "Create and launch the wedding website", "Include important details, RSVP functionality, and travel information" },
        { "saveTheDatesSent", "Save-the-Dates Sent", "Send out save-the-date cards", "Ensure all guests receive the cards well in advance" },
        { "invitationsSent", "Invitations Sent", "Send out wedding invitations", "Include RSVP cards and deadline for response" },
        { "ceremonyDetails", "Ceremony Details", "Finalize ceremony details", "Include officiant, vows, readings, and music" },
        { "receptionDetails", "Reception Details", "Finalize reception details", "Include seating arrangements, menu, and timeline" },
        { "necessaryDocumentsObtained", "Necessary Documents Obtained", "Obtain all necessary legal documents", "Include marriage license and any required permits" },
        { "dayOfTimeline", "Day-Of Timeline", "Create a detailed day-of timeline", "Include all important events and ensure smooth transitions" },
        { "transportationArranged", "Transportation Arranged", "Arrange transportation for the wedding party and guests", "Consider shuttles, limos, or rental cars" },
        { "rehearsalDinnerDetails", "Rehearsal Dinner Details", "Plan the rehearsal dinner", "Include location, menu, and guest list" },
        { "weddingRings", "Wedding Rings", "Purchase wedding rings", "Consider style, material, and engraving" },
        { "accommodations", "Accommodations", "Book accommodations for out-of-town guests", "Consider proximity to wedding venue and group rates" },
        { "hairMakeupTrialsScheduled", "Hair/Makeup Trials Scheduled", "Schedule hair and makeup trials", "Ensure the desired look is achieved" },
        { "honeymoonDetails", "Honeymoon Details", "Plan the honeymoon", "Consider destination, activities, and budget" },
        { "giftRegistry", "Gift Registry", "Create a gift registry", "Include a variety of price points and styles" },
        { "preWeddingCelebrations", "Pre-Wedding Celebrations", "Plan pre-wedding celebrations", "Include bachelor/bachelorette parties and bridal shower" },
        { "finalDetailsConfirmed", "Final Details Confirmed", "Confirm all final details with vendors", "Ensure all contracts are signed and payments are made" },
        { "seatingChart", "Seating Chart", "Create the seating chart", "Consider relationships and any special requirements" },
        { "welcomeBagsAssembled", "Welcome Bags Assembled", "Assemble welcome bags for out-of-town guests", "Include snacks, maps, and wedding weekend itinerary" },
        { "finalGuestCountConfirmed", "Final Guest Count Confirmed", "Confirm the final guest count with the caterer and venue", "Ensure accuracy for seating and catering purposes" },
        { "rehearsalConducted", "Rehearsal Conducted", "Conduct the wedding rehearsal", "Ensure all participants understand their roles and responsibilities" },
        { "hina", "Henna", "Plan the henna ceremony", "Consider location, decor, and guest list" },
        { "mikva", "Mikvah", "Schedule the mikvah appointment", "Ensure the bride attends the mikvah before the wedding" },
        { "weddingDate", "Wedding Date", "Set the final wedding date", "Consider season, venue availability, and guest availability" },
        { "weddingDayExecuted", "Wedding Day Executed", "Execute the wedding day timeline", "Ensure all events run smoothly and on schedule" },
        { "postWeddingTasksCompleted", "Post-Wedding Tasks Completed", "Complete all post-wedding tasks", "Include thank-you cards, vendor reviews, and legal name changes" }
    };

        DateTime? minTime = s_dal.Clock.GetStartProject();

        foreach (var v in s_dal.Task.ReadAll())
        {
            if (minTime < v.DeadlineDate)
            {
                minTime = v.DeadlineDate;
            }
        }

        DateTime weddingDate = new DateTime(2025, 1, 1);

        for (int i = 0; i < taskArr.GetLength(0); i++)
        {
            // Generate random values for task creation
            string? alias = taskArr[i, 0];
            string? description = taskArr[i, 1];
            DateTime createdDate = DateTime.Now;
            int cost = s_rand.Next(1000, 2000);
            bool isMilestone = (s_rand.Next(0, 1000) % 2) == 0 ? true : false;
            int userLvl = s_rand.Next(0, 4);

            DateTime startDate, scheduledDate, deadDate, completeDate;

            // Adjust dates based on the specific task
            switch (alias)
            {
                case "weddingDate":
                    // Wedding date remains as previously set
                    startDate = weddingDate.AddDays(-1);
                    scheduledDate = weddingDate;
                    deadDate = weddingDate;
                    completeDate = weddingDate;
                    break;
                case "mikva":
                    // Mikvah appointment scheduled 1 week before the wedding
                    startDate = weddingDate.AddDays(-8);
                    scheduledDate = weddingDate.AddDays(-7);
                    deadDate = weddingDate.AddDays(-6);
                    completeDate = weddingDate.AddDays(-6);
                    break;
                case "hina":
                    // Henna ceremony scheduled 2 days before the wedding
                    startDate = weddingDate.AddDays(-3);
                    scheduledDate = weddingDate.AddDays(-2);
                    deadDate = weddingDate.AddDays(-1);
                    completeDate = weddingDate.AddDays(-1);
                    break;
                case "rehearsalConducted":
                    // Rehearsal conducted 1 day before the wedding
                    startDate = weddingDate.AddDays(-2);
                    scheduledDate = weddingDate.AddDays(-1);
                    deadDate = weddingDate.AddDays(-1);
                    completeDate = weddingDate.AddDays(-1);
                    break;
                default:
                    // Schedule other tasks relative to the wedding date
                    int daysBeforeWedding = s_rand.Next(30, 180);
                    startDate = weddingDate.AddDays(-daysBeforeWedding);
                    scheduledDate = startDate.AddDays(s_rand.Next(1, 7));
                    deadDate = scheduledDate.AddDays(s_rand.Next(1, 7));
                    completeDate = deadDate;
                    break;
            }

            string deliverables = taskArr[i, 2];
            string remarks = taskArr[i, 3];

            // Create a new Task object and add it to the data access layer
            Task newTask = new(0, alias, description, cost, 0, 0, 0, createdDate, isMilestone, startDate, scheduledDate, deadDate, completeDate, deliverables, remarks, null, (DO.UserLevel)userLvl);
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
            "Mom Of Bride",
            "Father Of Bride",
            "Bride",
            "Mom Of Groom",
            "Father Of Groom",
            "Groom",
            "Temporary Friend",
            "Temporary Family Member",
            "Temporary Staff"
        };

        foreach (string user in users) {
            // Generate random values for user creation
            int id =s_rand.Next(200000000, 400000000); 
            int userLvl = s_rand.Next(0, 4);
            int phoneNumber = s_rand.Next(97200, 97299);
            string Password = "123456";
            string nate = user + "@gmail.com";

            // Create a new User object and add it to the data access layer
            User newUser = new(id, nate, phoneNumber.ToString(), user, Password, (DO.UserLevel)userLvl);
            s_dal!.User.Create(newUser);
        }
        User DemoUser = new(212451074, "Ephraim.Elgrabli@gmail.com","0501234567","Ephraim Elgrabli","Admin", DO.UserLevel.Producer);
        s_dal!.User.Create(DemoUser);
        User DemoUser1 = new(328301981, "Maor.Levi@gmail.com", "0501234567", "Maor Levi", "Admin", DO.UserLevel.Producer);
        s_dal!.User.Create(DemoUser1);
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

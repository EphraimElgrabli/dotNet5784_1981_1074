namespace BO;

/// <summary>
/// Enum representing the different levels of user involvement in a wedding management app
/// </summary>
public enum UserLevel
{
    Friends,                  // Friends providing supporting roles in the wedding
    Staff,                    // Close friends of the couple actively involved in wedding preparations
    FamilyMember,             // Immediate family members participating in the wedding
    FamilyMemberWithRoles,    // Immediate family members with specific assigned roles in the wedding
    Producer,                  // Wedding producer or planner overseeing the entire event
    None
}

// Enum representing the status of a wedding task in the wedding management app
public enum Status
{
    Unscheduled,     // Task is not yet scheduled
    Scheduled,       // Task is scheduled for execution
    OnTrack,         // Task is progressing as planned     
    Done,             // Task has been successfully completed
    InJeopardy
}
public enum StatusProject
{
    UnStarted,     // project is not yet scheduled
    InProgress,     // project is not yet scheduled
    Done,       //project is scheduled for execution
}
namespace DO;

// Enum representing the different levels of user involvement in a wedding app
public enum UserLevel
{
    supportes,       // Supporting role
    closeFriends,    // Close friends of the couple
    bride,           // The bride
    groom,           // The groom
    producer         // Wedding producer or planner
}

// Enum representing the status of a wedding task in the app
public enum Status
{
    Unscheduled,     // Task is not yet scheduled
    Scheduled,       // Task is scheduled
    OnTrack,         // Task is on track
    InJeopardy,      // Task is in jeopardy or at risk
    Done             // Task is completed
}
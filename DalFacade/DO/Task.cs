using System.Collections.Generic;

namespace DO;
public record Task
(
    int Id,
    string Alias,
    string Description,
    DateTime CreatedAtDate,
    bool IsMilestone = false,
    DateTime? StartDate = null,
    DateTime? ScheduledDate = null,
    DateTime? DeadlineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverables = null,
    string? Remarks = null,
    int? UserId = null,
    DO.UserLevel? Copmlexity = null
)
{
    
    public Task() : this(0, "", "", DateTime.Now) { } //empty ctor for stage 3

    /// <summary>
    /// RegistrationDate - registration date of the current student record
    /// </summary>
    public DateTime RegistrationDate => DateTime.Now; //get only
}

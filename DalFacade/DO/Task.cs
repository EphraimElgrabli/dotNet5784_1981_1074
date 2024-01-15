using System.Collections.Generic;
namespace DO;

/// <summary>
/// Represents a task record with various properties.
/// </summary>
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
    DO.UserLevel? Complexity = null
)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Task"/> class.
    /// </summary>
    /// <param name="Id">The unique identifier for the task.</param>
    public Task() : this(0, "", "", DateTime.Now) { } // Empty constructor for stage 3

    /// <summary>
    /// Gets the registration date of the current task record.
    /// </summary>
    public DateTime RegistrationDate => DateTime.Now; // Get-only property
}

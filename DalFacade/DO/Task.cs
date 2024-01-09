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

/// <summary>
/// Represents a task in the wedding management app.
/// </summary>
/// <param name="Id">The unique identifier for the task.</param>
/// <param name="Alias">The alias or short name of the task.</param>
/// <param name="Description">The description of the task.</param>
/// <param name="CreatedAtDate">The date when the task was created.</param>
/// <param name="IsMilestone">Indicates whether the task is a milestone (default is false).</param>
/// <param name="StartDate">The start date of the task (nullable).</param>
/// <param name="ScheduledDate">The scheduled date for the task (nullable).</param>
/// <param name="DeadlineDate">The deadline date for the task (nullable).</param>
/// <param name="CompleteDate">The completion date of the task (nullable).</param>
/// <param name="Deliverables">The deliverables associated with the task (nullable).</param>
/// <param name="Remarks">Additional remarks or details about the task (nullable).</param>
/// <param name="UserId">The user ID associated with the task (nullable).</param>
/// <param name="Complexity">The complexity level of the task (nullable).</param>
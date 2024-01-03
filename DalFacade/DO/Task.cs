namespace DO;
public record Task
(
    int Id,
    string Alias,
    string Description,
    DateTime CreatedAtDate,
    TimeSpan RequiredEffortTime,
    bool IsMilestone,
    DO.EngineerExperience Copmlexity,
    DateTime StartDate,
    DateTime ScheduledDate,
    DateTime DeadlineDate,
    DateTime CompleteDate,
    string Deliverables,
    string Remarks,
    int EngineerId  //conect to engineerid
)
{
    public Task() : this(0) { } //empty ctor for stage 3

    /// <summary>
    /// RegistrationDate - registration date of the current student record
    /// </summary>
    public DateTime RegistrationDate => DateTime.Now; //get only
}
